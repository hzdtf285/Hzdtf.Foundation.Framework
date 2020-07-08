using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Rabbit.Model.Standard.Connection;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using Hzdtf.Utility.Standard.Release;
using Hzdtf.Utility.Standard.Data;
using System.Threading;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit RPC客户端
    /// @ 黄振东
    /// </summary>
    public class RabbitRpcClient : IRpcClient, ICloseable, IDisposable
    {
        #region 属性与字段

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        /// <summary>
        /// 渠道
        /// </summary>
        private readonly IModel _channel;

        /// <summary>
        /// 回复队列名
        /// </summary>
        private readonly string _replyQueueName;

        /// <summary>
        /// 回复消费者
        /// </summary>
        private readonly EventingBasicConsumer _consumer;

        /// <summary>
        /// 回调映射
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<byte[]>> _callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<byte[]>>();

        /// <summary>
        /// Rabbit消息队列信息
        /// </summary>
        private readonly RabbitMessageQueueInfo rabbitMessageQueueInfo;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// 初始化各个对象以便就绪
        /// 只初始化交换机与基本属性，队列定义请重写Init方法进行操作
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitRpcClient(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
        {
            ValidateUtil.ValidateNull(channel, "渠道");
            ValidateUtil.ValidateNull(rabbitMessageQueueInfo, "消息队列信息");

            this._channel = channel;
            this.rabbitMessageQueueInfo = rabbitMessageQueueInfo;
            _replyQueueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(_replyQueueName, rabbitMessageQueueInfo.Exchange, _replyQueueName); 
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out TaskCompletionSource<byte[]> tcs))
                {
                    return;
                }

                if (ea.Body.IsEmpty)
                {
                    tcs.TrySetResult(null);
                }
                else
                {
                    tcs.TrySetResult(ea.Body.ToArray());
                }
            };
        }

        /// <summary>
        /// 构造方法
        /// 初始化各个对象以便就绪
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <param name="virtualPath">虚拟路径</param>
        public RabbitRpcClient(IModel channel, string queueOrOtherIdentify, IMessageQueueInfoFactory messageQueueInfoFactory, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
            : this(channel, RabbitMessageQueueInfo.From(messageQueueInfoFactory.Create(queueOrOtherIdentify, ConfigUtil.CreateContainerVirtualPathDic(virtualPath))))
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
            return CallAsync(message).Result;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>返回字节流任务</returns>
        public Task<byte[]> CallAsync(byte[] message)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            IBasicProperties props = _channel.CreateBasicProperties();
            var correlationId = StringUtil.NewShortGuid();
            props.CorrelationId = correlationId;
            props.ReplyTo = _replyQueueName;
            var tcs = new TaskCompletionSource<byte[]>();
            _callbackMapper.TryAdd(correlationId, tcs);

            _channel.BasicPublish(
                exchange: rabbitMessageQueueInfo.Exchange,
                routingKey: rabbitMessageQueueInfo.Queue,
                basicProperties: props,
                body: message);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);

            cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out var tmp));
            return tcs.Task;
        }

        #endregion

        #region ICloseable 接口

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (_channel != null && _channel.IsOpen)
            {
                _channel.Close();
                _channel.Dispose();
            }

            OnClosed();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 执行关闭事件
        /// </summary>
        /// <param name="data">数据</param>
        protected void OnClosed(object data = null)
        {
            if (Closed != null)
            {
                Closed(this, new DataEventArgs(data));
            }
        }

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// 析构方法
        /// </summary>
        ~RabbitRpcClient()
        {
            Dispose();
        }
    }
}