using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Rabbit.Model.Standard.Utils;
using System.Linq;
using Hzdtf.Rabbit.Model.Standard.Connection;

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

        /// <summary>
        /// 根据虚拟路径获取交换机列表
        /// </summary>
        /// <param name="rabbitConfig">rabbit配置</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>交换机列表</returns>
        public static IList<RabbitExchangeInfo> GetExchanges(this RabbitConfigInfo rabbitConfig, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            if (rabbitConfig == null)
            {
                return null;
            }
            
            var virtObj = rabbitConfig.Rabbit.Where(p => p.VirtualPath == virtualPath).FirstOrDefault();
            if (virtObj == null)
            {
                return null;
            }

            return virtObj.Exchanges;
        }

        /// <summary>
        /// 将交换机信息列表转换为消息队列信息列表
        /// </summary>
        /// <param name="exchanges">交换机信息列表</param>
        /// <returns>消息队列信息列表</returns>
        public static IList<RabbitMessageQueueInfo> ToMessageQueues(this IList<RabbitExchangeInfo> exchanges)
        {
            if (exchanges.IsNullOrCount0())
            {
                return null;
            }

            IList<RabbitMessageQueueInfo> result = new List<RabbitMessageQueueInfo>();
            foreach (RabbitExchangeInfo item in exchanges)
            {
                if (item.Queues.IsNullOrCount0())
                {
                    result.Add(ToMessageQueue(item));
                }
                else
                {
                    foreach (RabbitQueueModel item2 in item.Queues)
                    {
                        RabbitMessageQueueInfo model = ToMessageQueue(item);
                        model.AutoDelQueue = item2.AutoDelQueue;
                        model.Qos = item2.Qos;
                        model.Queue = item2.Name;
                        model.RoutingKeys = item2.RoutingKeys;

                        result.Add(model);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 创建消息队列信息并赋基本值
        /// </summary>
        /// <param name="rabbitExchangeInfo">Rabbit交换机信息</param>
        /// <returns>Rabbit消息队列信息</returns>
        public static RabbitMessageQueueInfo ToMessageQueue(this RabbitExchangeInfo rabbitExchangeInfo)
        {
            RabbitMessageQueueInfo model = new RabbitMessageQueueInfo();
            model.Exchange = rabbitExchangeInfo.Name;
            model.Type = rabbitExchangeInfo.Type;
            model.Persistent = rabbitExchangeInfo.Persistent;

            return model;
        }
    }
}
