using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Rabbit.Model.Standard.Utils;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列辅助类
    /// @ 黄振东
    /// </summary>
    public static class RabbitMessageQueueUtil
    {
        /// <summary>
        /// 根据队列读取消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="allList">所有列表</param>
        /// <returns>消息队列信息</returns>
        public static RabbitMessageQueueInfo Reader(string queueOrOtherIdentify, IList<RabbitMessageQueueInfo> allList)
        {
            if (string.IsNullOrWhiteSpace(queueOrOtherIdentify))
            {
                throw new ArgumentNullException("队列或其他标识不能为空");
            }

            RabbitMessageQueueInfo info = null;
            if (queueOrOtherIdentify.Contains(":"))
            {
                var names = queueOrOtherIdentify.Split(':');
                if (string.IsNullOrWhiteSpace(names[0]) || string.IsNullOrWhiteSpace(names[1]))
                {
                    throw new ArgumentNullException("交换机和队列不能为空");
                }

                info = ReaderByExchangeAndQueue(names[0], names[1], allList);
            }
            else
            {
                // 先按队列名去找，找不到再按交换机名去找
                info = ReaderByQueue(queueOrOtherIdentify, allList);
                if (info == null)
                {
                    info = ReaderByExchange(queueOrOtherIdentify, allList);
                }
            }

            return info;
        }

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="allList">所有列表</param>
        /// <returns>消息队列信息</returns>
        public static RabbitMessageQueueInfo ReaderByExchange(string exchange, IList<RabbitMessageQueueInfo> allList)
        {
            if (allList.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in allList)
            {
                if (RabbitUtil.IsTwoExchangeEqual(exchange, item.Exchange))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <param name="allList">所有列表</param>
        /// <returns>消息队列信息</returns>
        public static RabbitMessageQueueInfo ReaderByQueue(string queue, IList<RabbitMessageQueueInfo> allList)
        {
            if (string.IsNullOrWhiteSpace(queue) || allList.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in allList)
            {
                if (queue.Equals(item.Queue))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据交换机和队列获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列</param>
        /// <param name="allList">所有列表</param>
        /// <returns>消息队列信息</returns>
        public static RabbitMessageQueueInfo ReaderByExchangeAndQueue(string exchange, string queue, IList<RabbitMessageQueueInfo> allList)
        {
            if (string.IsNullOrWhiteSpace(exchange) || string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            return GetMessagQueueInfoByCondition(x =>
            {
                return RabbitUtil.IsTwoExchangeEqual(exchange, x.Exchange) && queue.Equals(x.Queue);
            }, allList);
        }

        /// <summary>
        /// 根据条件获取消息队列信息
        /// </summary>
        /// <param name="func">回调条件</param>
        /// <param name="allList">所有列表</param>
        /// <returns>消息队列信息</returns>
        public static RabbitMessageQueueInfo GetMessagQueueInfoByCondition(Func<RabbitMessageQueueInfo, bool> func, IList<RabbitMessageQueueInfo> allList)
        {
            if (allList.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in allList)
            {
                if (func(item))
                {
                    return item;
                }
            }

            return null;
        }
    }
}
