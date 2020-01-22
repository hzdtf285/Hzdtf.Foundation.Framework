using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Core
{
    /// <summary>
    /// 生产者接口
    /// @ 黄振东
    /// </summary>
    public interface IProducer : ICloseable
    {
        /// <summary>
        /// 字节数组序列化
        /// </summary>
        IBytesSerialization BytesSerialization
        {
            get;
            set;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布路由键</param>
        void Publish(string message, string publishRoutingKey = null);

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布路由键</param>
        void Publish(object message, string publishRoutingKey = null);

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布路由键</param>
        void Publish(byte[] message, string publishRoutingKey = null);
    }
}
