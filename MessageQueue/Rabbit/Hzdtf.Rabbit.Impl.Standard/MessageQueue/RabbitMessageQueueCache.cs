using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列缓存
    /// @ 黄振东
    /// </summary>
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
            return RabbitMessageQueueUtil.Reader(queueOrOtherIdentify, GetAllList());
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
            return RabbitMessageQueueUtil.ReaderByExchange(exchange, GetAllList());
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue)
        {
            return RabbitMessageQueueUtil.ReaderByQueue(queue, GetAllList());
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

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns>所有列表</returns>
        private IList<RabbitMessageQueueInfo> GetAllList()
        {
            return dicCaches.Count == 0 ? ReaderAll() : dicCaches.ValuesToList();
        }

        #endregion
    }
}
