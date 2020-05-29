using Hzdtf.Utility.Standard.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 连接转换器扩展类
    /// @ 黄振东
    /// </summary>
    public static class ConnectionMultiplexerExtend
    {
        /// <summary>
        /// 锁住可用的资源，此方法是发布订阅模式，性能较好，推荐使用
        /// </summary>
        /// <param name="connectionMultiplexer">连接转换器，此处的连接器必须是可写的DB</param>
        /// <param name="key">键</param>
        /// <param name="action">动作</param>
        /// <param name="dbIndex">数据库索引</param>
        /// <param name="timeoutMilliSecond">超时毫秒，默认为5秒</param>
        /// <param name="flags">命令标记</param>
        public static void LockTake(this IConnectionMultiplexer connectionMultiplexer, RedisKey key, Action action, int dbIndex = -1, int timeoutMilliSecond = 5000, CommandFlags flags = CommandFlags.None)
        {
            var channel = StringUtil.NewShortGuid();
            var subscriber = connectionMultiplexer.GetSubscriber();
            var db = connectionMultiplexer.GetDatabase(dbIndex);

            // 将进入的都加入到队列里，将频道唯一值作为队列值
            var count = db.ListLeftPush(key, channel, flags: flags);

            // 如果大于1，说明已经被其他地方锁定资源，需要监听等待触发释放锁
            if (count > 1)
            {
                // 是否已执行业务
                var isExecuted = false;
                subscriber.SubscribeAsync(channel, (myChannel, channelValue) =>
                {
                    if (isExecuted)
                    {
                        subscriber.UnsubscribeAsync(channel, flags: flags);
                        return;
                    }

                    isExecuted = true;
                    subscriber.UnsubscribeAsync(channel, flags: flags);
                    ExecLockAction(subscriber, db, key, action, flags);
                }, flags: flags);

                // 等待超时，如果超时后还未执行，则手动执行，预防死锁
                Thread.Sleep(timeoutMilliSecond);
                if (isExecuted)
                {
                    return;
                }
                else
                {
                    isExecuted = true;
                    // 移除队列的元素
                    db.ListRemoveAsync(key, channel, flags: flags);
                    subscriber.UnsubscribeAsync(channel, flags: flags);
                    ExecLockAction(subscriber, db, key, action, flags);
                }
            }
            else
            {
                // 排除第1个队列值
                db.ListRightPop(key, flags: flags);
                ExecLockAction(subscriber, db, key, action, flags);
            } 
        }

        /// <summary>
        /// 执行锁住后的动作
        /// </summary>
        /// <param name="subscriber">订阅者</param>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="action">动作</param>
        /// <param name="flags">命令标记</param>
        private static void ExecLockAction(ISubscriber subscriber, IDatabase db, RedisKey key, Action action, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                var nextValue = db.ListRightPop(key, flags: flags);
                if (!nextValue.IsNull)
                {
                    // 通知下一个监听执行
                    subscriber.Publish(nextValue.ToString(), RedisValue.EmptyString, flags: flags);
                }
            }
        }
    }
}
