using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列基类
    /// @ 黄振东
    /// </summary>
    public abstract class RabbitMessageQueueBase : IRabbitMessageQueueReader
    {
        #region IRabbitMessageQueueReader 接口

        /// <summary>
        /// 根据队列读取消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo Reader(string queueOrOtherIdentify)
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

                info = ReaderByExchangeAndQueue(names[0], names[1]);
            }
            else
            {
                // 先按队列名去找，找不到再按交换机名去找
                info = ReaderByQueue(queueOrOtherIdentify);
                if (info == null)
                {
                    info = ReaderByExchange(queueOrOtherIdentify);
                }
            }

            return info;
        }

        /// <summary>
        /// 读取全部消息队列信息列表
        /// </summary>
        /// <returns>全部消息队列信息列表</returns>
        public IList<RabbitMessageQueueInfo> ReaderAll()
        {
            return Trans(QueryExchangeInfosFromSource());
        }

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchange(string exchange)
        {
            return GetMessagQueueInfoByCondition(x =>
            {
                return RabbitUtil.IsTwoExchangeEqual(exchange, x.Exchange);
            });
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue)
        {
            if (string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            return GetMessagQueueInfoByCondition(x =>
            {
                return queue.Equals(x.Queue);
            });
        }

        /// <summary>
        /// 根据交换机和队列获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchangeAndQueue(string exchange, string queue)
        {
            if (string.IsNullOrWhiteSpace(exchange) || string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            return GetMessagQueueInfoByCondition(x =>
            {
                return RabbitUtil.IsTwoExchangeEqual(exchange, x.Exchange) && queue.Equals(x.Queue);
            });
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 从源头查询交换机信息列表
        /// </summary>
        /// <returns>交换机信息列表</returns>
        protected abstract IList<RabbitExchangeInfo> QueryExchangeInfosFromSource();

        #endregion

        #region 私有方法

        /// <summary>
        /// 将交换机信息列表转换为消息队列信息列表
        /// </summary>
        /// <param name="exchanges">交换机信息列表</param>
        /// <returns>消息队列信息列表</returns>
        private IList<RabbitMessageQueueInfo> Trans(IList<RabbitExchangeInfo> exchanges)
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
                    result.Add(CreateBasicProps(item));
                }
                else
                {
                    foreach (RabbitQueueModel item2 in item.Queues)
                    {
                        RabbitMessageQueueInfo model = CreateBasicProps(item);
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
        private RabbitMessageQueueInfo CreateBasicProps(RabbitExchangeInfo rabbitExchangeInfo)
        {
            RabbitMessageQueueInfo model = new RabbitMessageQueueInfo();
            model.Exchange = rabbitExchangeInfo.Name;
            model.Type = rabbitExchangeInfo.Type;
            model.Persistent = rabbitExchangeInfo.Persistent;

            return model;
        }

        /// <summary>
        /// 根据条件获取消息队列信息
        /// </summary>
        /// <param name="func">回调条件</param>
        /// <returns>消息队列信息</returns>
        private RabbitMessageQueueInfo GetMessagQueueInfoByCondition(Func<RabbitMessageQueueInfo, bool> func)
        {
            IList<RabbitMessageQueueInfo> list = ReaderAll();
            if (list.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in list)
            {
                if (func(item))
                {
                    return item;
                }
            }

            return null;
        }

        #endregion
    }
}