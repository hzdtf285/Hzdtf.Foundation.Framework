using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.MessageQueue
{
    /// <summary>
    /// 消息队列读取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IMessageQueueReader<T> where T : MessageQueueInfo
    {
        /// <summary>
        /// 根据队列读取消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消息队列信息</returns>
        T Reader(string queueOrOtherIdentify);

        /// <summary>
        /// 读取全部消息队列信息列表
        /// </summary>
        /// <returns>全部消息队列信息列表</returns>
        IList<T> ReaderAll();
    }
}
