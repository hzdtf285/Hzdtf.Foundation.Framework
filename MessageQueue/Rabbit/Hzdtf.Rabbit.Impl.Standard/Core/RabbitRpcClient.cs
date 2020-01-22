using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.ProcessCall;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit RPC客户端
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitRpcClient : RabbitCoreBase, IRpcClient
    {
        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        public RabbitRpcClient(IModel channel, IMessageQueueInfoFactory messageQueueInfoFactory)
            : base(channel, messageQueueInfoFactory)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitRpcClient(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
            : base(channel, rabbitMessageQueueInfo)
        {
        }

        #endregion

        #region IRpcClient 接口

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>返回字节流</returns>
        public byte[] Call(byte[] message)
        {
            return new SimpleRpcClient(channel, rabbitMessageQueueInfo.Queue).Call(message);
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            channel.QueueDeclare(rabbitMessageQueueInfo.Queue, rabbitMessageQueueInfo.Persistent, false, rabbitMessageQueueInfo.AutoDelQueue, null);

            if (rabbitMessageQueueInfo.RoutingKeys.IsNullOrLength0())
            {
                return;
            }

            foreach (string key in rabbitMessageQueueInfo.RoutingKeys)
            {
                channel.QueueBind(rabbitMessageQueueInfo.Queue, rabbitMessageQueueInfo.Exchange, key);
            }
        }

        #endregion
    }
}