using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列信息配置工厂
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueInfoConfigFactory : IMessageQueueInfoFactory
    {
        #region 属性与字段

        /// <summary>
        /// 消息队列读取
        /// </summary>
        public IRabbitMessageQueueReader MessageQueueReader
        {
            get;
            set;
        } = new RabbitMessageQueueCache();

        #endregion

        #region IMessageQueueInfoFactory 接口

        /// <summary>
        /// 创建消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消息队列信息</returns>
        public MessageQueueInfo Create(string queueOrOtherIdentify = null)
        {
            return MessageQueueReader.Reader(queueOrOtherIdentify);
        }

        #endregion
    }
}
