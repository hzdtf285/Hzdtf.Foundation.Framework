using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.ProcessCall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Connection
{
    /// <summary>
    /// 消息队列连接接口
    /// @ 黄振东
    /// </summary>
    public interface IMessageQueueConnection : IConnection
    {
        #region 生产者

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>生产者</returns>
        IProducer CreateProducer(string queueOrOtherIdentify);

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="MessageQueueInfo">消息队列信息</param>
        /// <returns>生产者</returns>
        IProducer CreateProducer(MessageQueueInfo MessageQueueInfo);

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="MessageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>生产者</returns>
        IProducer CreateProducer(string queueOrOtherIdentify, IMessageQueueInfoFactory MessageQueueInfoFactory);

        #endregion

        #region 消费者

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消费者</returns>
        IConsumer CreateConsumer(string queueOrOtherIdentify);

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="MessageQueueInfo">消息队列信息</param>
        /// <returns>消费者</returns>
        IConsumer CreateConsumer(MessageQueueInfo MessageQueueInfo);

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="MessageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>消费者</returns>
        IConsumer CreateConsumer(string queueOrOtherIdentify, IMessageQueueInfoFactory MessageQueueInfoFactory);

        #endregion

        #region RPC客户端

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>RPC客户端</returns>
        IRpcClient CreateRpcClient(string queueOrOtherIdentify);

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="MessageQueueInfo">消息队列信息</param>
        /// <returns>RPC客户端</returns>
        IRpcClient CreateRpcClient(MessageQueueInfo MessageQueueInfo);

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="MessageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC客户端</returns>
        IRpcClient CreateRpcClient(string queueOrOtherIdentify, IMessageQueueInfoFactory MessageQueueInfoFactory);

        #endregion

        #region RPC服务端

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>RPC服务端</returns>
        IRpcServer CreateRpcServer(string queueOrOtherIdentify);

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="MessageQueueInfo">消息队列信息</param>
        /// <returns>RPC服务端</returns>
        IRpcServer CreateRpcServer(MessageQueueInfo MessageQueueInfo);

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="MessageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC服务端</returns>
        IRpcServer CreateRpcServer(string queueOrOtherIdentify, IMessageQueueInfoFactory MessageQueueInfoFactory);

        #endregion
    }
}
