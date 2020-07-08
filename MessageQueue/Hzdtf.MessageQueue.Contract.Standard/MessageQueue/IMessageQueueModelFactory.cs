using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.MessageQueue
{
    /// <summary>
    /// 消息队列信息工厂
    /// @ 黄振东
    /// </summary>
    public interface IMessageQueueInfoFactory
    {
        /// <summary>
        /// 创建消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="extend">扩展</param>
        /// <returns>消息队列信息</returns>
        MessageQueueInfo Create(string queueOrOtherIdentify = null, IDictionary<string, string> extend = null);
    }
}
