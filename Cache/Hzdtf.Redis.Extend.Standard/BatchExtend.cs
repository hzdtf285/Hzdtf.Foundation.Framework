using StackExchange.Redis;
using System;
using Hzdtf.Utility.Standard.Utils;
using System.Threading.Tasks;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 批量扩展类
    /// @ 黄振东
    /// </summary>
    public static class BatchExtend
    {
        /// <summary>
        /// 对象设置
        /// </summary>
        /// <param name="batch">批量</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <param name="flags">命令标记</param>
        /// <returns>任务</returns>
        public static Task ObjectSetAsync(this IBatch batch, RedisKey key, object value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None)
        {
            HashEntry[] hashEntries = RedisUtil.ToHashEntrys(value);
            if (hashEntries.IsNullOrLength0())
            {
                return Task.Factory.StartNew(() => { });
            }
            
            batch.KeyDeleteAsync(key, flags: flags);

            Task task = batch.HashSetAsync(key, hashEntries, flags: flags);
            if (expiry != null)
            {
                batch.KeyExpireAsync(key, expiry, flags: flags);
            }

            return task;
        }
    }
}
