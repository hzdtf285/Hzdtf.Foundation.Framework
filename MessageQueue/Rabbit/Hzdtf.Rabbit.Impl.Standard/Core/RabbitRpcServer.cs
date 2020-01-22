using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.ProcessCall;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit RPC服务端
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitRpcServer : RabbitCoreBase, IRpcServer
    {
        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        public RabbitRpcServer(IModel channel, IMessageQueueInfoFactory messageQueueInfoFactory)
            : base(channel, messageQueueInfoFactory)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitRpcServer(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
            : base(channel, rabbitMessageQueueInfo)
        {
        }

        #endregion

        #region IRpcServer 接口

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        public void Receive(Func<byte[], byte[]> receiveMessageFun)
        {
            RabbitSimpleRpcServer rabbitSimpleRpcServer = new RabbitSimpleRpcServer(new Subscription(channel, rabbitMessageQueueInfo.Queue), receiveMessageFun, Log);
            rabbitSimpleRpcServer.MainLoop();
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

    /// <summary>
    /// Rabbit简单 RPC服务端
    /// @ 黄振东
    /// </summary>
    class RabbitSimpleRpcServer : SimpleRpcServer
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 接收消息回调
        /// </summary>
        private readonly Func<byte[], byte[]> receiveMessageFun;

        /// <summary>
        /// 返回客户端消息
        /// </summary>
        private byte[] returnClientMessage;

        /// <summary>
        /// 本身名称
        /// </summary>
        private static readonly string thisName = typeof(RabbitSimpleRpcServer).Name;

        /// <summary>
        /// 本身全名称
        /// </summary>
        private static readonly string thisFullName = typeof(RabbitSimpleRpcServer).FullName;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="subscription">消费</param>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="log">日志</param>
        public RabbitSimpleRpcServer(Subscription subscription, Func<byte[], byte[]> receiveMessageFun, ILogable log) : base(subscription)
        {
            this.receiveMessageFun = receiveMessageFun;
            this.log = log;
        }

        /// <summary>
        /// 执行完成后进行回调
        /// </summary>
        /// <param name="isRedelivered">是否传递</param>
        /// <param name="requestProperties">请求属性</param>
        /// <param name="body">由客户端发送过来的字节流</param>
        /// <param name="replyProperties">回复属性</param>
        /// <returns>需要返回给客户端的字节流</returns>
        public override byte[] HandleSimpleCall(bool isRedelivered, IBasicProperties requestProperties, byte[] body, out IBasicProperties replyProperties)
        {
            replyProperties = null;

            // 这里是返回给客户端
            return returnClientMessage;
        }

        /// <summary>
        /// 进行处理
        /// </summary>
        /// <param name="evt">基本传递事件参数</param>
        public override void ProcessRequest(BasicDeliverEventArgs evt)
        {
            try
            {
                if (receiveMessageFun != null)
                {
                    // 这里调用处理业务
                    returnClientMessage = receiveMessageFun(evt.Body);
                }

                base.ProcessRequest(evt);
            }
            catch (Exception ex)
            {
                log.ErrorAsync(ex.Message, ex, thisName, thisFullName);
            }
        }
    }
}
