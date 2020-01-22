using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Rabbit.Model.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitMessageQueueCache : SingleTypeLocalMemoryBase<string, RabbitMessageQueueInfo>, IRabbitMessageQueueReader
    {
        #region 属性与字段

        /// <summary>
        /// 原生消息队列读取
        /// </summary>
        public IRabbitMessageQueueReader ProtoMessageQueueReader
        {
            get;
            set;
        } = new RabbitMessageQueueJson();

        /// <summary>
        /// 缓存键（以队列名为键）
        /// </summary>
        private static readonly IDictionary<string, RabbitMessageQueueInfo> dicCaches = new Dictionary<string, RabbitMessageQueueInfo>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        #endregion

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
            if (dicCaches.Count == 0)
            {
                IList<RabbitMessageQueueInfo> list = ProtoMessageQueueReader.ReaderAll();
                if (list.IsNullOrCount0())
                {
                    return null;
                }

                foreach (RabbitMessageQueueInfo item in list)
                {
                    Add(item.Queue, item);
                }

                return list;
            }
            else
            {
                return dicCaches.ValuesToList();
            }
        }

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchange(string exchange)
        {
            IList<RabbitMessageQueueInfo> list = dicCaches.Count == 0 ? ReaderAll() : dicCaches.ValuesToList();
            if (list.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in list)
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
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue)
        {
            if (string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            IList<RabbitMessageQueueInfo> list = dicCaches.Count == 0 ? ReaderAll() : dicCaches.ValuesToList();
            if (list.IsNullOrCount0())
            {
                return null;
            }

            foreach (RabbitMessageQueueInfo item in list)
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

        #region 重写父类的方法

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns>缓存对象</returns>
        protected override IDictionary<string, RabbitMessageQueueInfo> GetCache()
        {
            return dicCaches;
        }

        /// <summary>
        /// 获取同步缓存对象
        /// </summary>
        /// <returns>同步缓存对象</returns>
        protected override object GetSyncCache()
        {
            return syncDicCaches;
        }

        #endregion

        #region 私有方法

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
