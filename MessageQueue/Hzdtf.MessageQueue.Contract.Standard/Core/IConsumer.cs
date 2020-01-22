using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Core
{
    /// <summary>
    /// 消费者接口
    /// @ 黄振东
    /// </summary>
    public interface IConsumer : ICloseable
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
        /// 业务异常生产者
        /// </summary>
        IProducer BusinessExceptionProducer
        {
            get;
            set;
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        void Subscribe(Func<string, bool> receiveMessageFun, bool isAutoAck = false);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T">接收类型</typeparam>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        void Subscribe<T>(Func<T, bool> receiveMessageFun, bool isAutoAck = false);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        void Subscribe(Func<byte[], bool> receiveMessageFun, bool isAutoAck = false);
    }
}
