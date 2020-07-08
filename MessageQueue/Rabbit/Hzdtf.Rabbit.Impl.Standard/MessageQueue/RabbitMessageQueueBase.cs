using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Rabbit.Model.Standard.Connection;

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
        /// <param name="extend">扩展</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo Reader(string queueOrOtherIdentify, IDictionary<string, string> extend = null)
        {
            if (string.IsNullOrWhiteSpace(queueOrOtherIdentify))
            {
                throw new ArgumentNullException("队列或其他标识不能为空");
            }

            var virtualPath = extend.GetVirtualPath();

            RabbitMessageQueueInfo info = null;
            if (queueOrOtherIdentify.Contains(":"))
            {
                var names = queueOrOtherIdentify.Split(':');
                if (string.IsNullOrWhiteSpace(names[0]) || string.IsNullOrWhiteSpace(names[1]))
                {
                    throw new ArgumentNullException("交换机和队列不能为空");
                }

                info = ReaderByExchangeAndQueue(names[0], names[1], virtualPath);
            }
            else
            {
                // 先按队列名去找，找不到再按交换机名去找
                info = ReaderByQueue(queueOrOtherIdentify, virtualPath);
                if (info == null)
                {
                    info = ReaderByExchange(queueOrOtherIdentify, virtualPath);
                }
            }

            return info;
        }

        /// <summary>
        /// 读取全部消息队列信息列表
        /// </summary>
        /// <param name="extend">扩展</param>
        /// <returns>全部消息队列信息列表</returns>
        public IList<RabbitMessageQueueInfo> ReaderAll(IDictionary<string, string> extend = null)
        {
            return QueryExchangeInfosFromSource(extend.GetVirtualPath()).ToMessageQueues();
        }

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchange(string exchange, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            var extend = ConfigUtil.CreateContainerVirtualPathDic(virtualPath);
            return GetMessagQueueInfoByCondition(x =>
            {
                return RabbitUtil.IsTwoExchangeEqual(exchange, x.Exchange);
            }, extend);
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            if (string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            var extend = ConfigUtil.CreateContainerVirtualPathDic(virtualPath);

            return GetMessagQueueInfoByCondition(x =>
            {
                return queue.Equals(x.Queue);
            }, extend);
        }

        /// <summary>
        /// 根据交换机和队列获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>消息队列信息</returns>
        protected RabbitMessageQueueInfo ReaderByExchangeAndQueue(string exchange, string queue, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            if (string.IsNullOrWhiteSpace(exchange) || string.IsNullOrWhiteSpace(queue))
            {
                return null;
            }

            var extend = ConfigUtil.CreateContainerVirtualPathDic(virtualPath);

            return GetMessagQueueInfoByCondition(x =>
            {
                return RabbitUtil.IsTwoExchangeEqual(exchange, x.Exchange) && queue.Equals(x.Queue);
            }, extend);
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 从源头查询交换机信息列表
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>交换机信息列表</returns>
        protected abstract IList<RabbitExchangeInfo> QueryExchangeInfosFromSource(string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH);

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据条件获取消息队列信息
        /// </summary>
        /// <param name="func">回调条件</param>
        /// <param name="extend">扩展</param>
        /// <returns>消息队列信息</returns>
        private RabbitMessageQueueInfo GetMessagQueueInfoByCondition(Func<RabbitMessageQueueInfo, bool> func, IDictionary<string, string> extend = null)
        {
            IList<RabbitMessageQueueInfo> list = ReaderAll(extend);
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