using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Rabbit.Model.Standard.Connection;
using RabbitMQ.Client.Events;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Release;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Logger.Contract.Standard;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit RPC服务端
    /// @ 黄振东
    /// </summary>
    public class RabbitRpcServer : IRpcServer, ICloseable, IDisposable
    {
        #region 属性与字段

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        /// <summary>
        /// 渠道
        /// </summary>
        private readonly IModel _channel;

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
        public RabbitRpcServer(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
        {
            ValidateUtil.ValidateNull(channel, "渠道");
            ValidateUtil.ValidateNull(rabbitMessageQueueInfo, "消息队列信息");

            this._channel = channel;
            this.rabbitMessageQueueInfo = rabbitMessageQueueInfo;
        }

        /// <summary>
        /// 构造方法
        /// 初始化各个对象以便就绪
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <param name="virtualPath">虚拟路径</param>
        public RabbitRpcServer(IModel channel, string queueOrOtherIdentify, IMessageQueueInfoFactory messageQueueInfoFactory, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
            : this(channel, RabbitMessageQueueInfo.From(messageQueueInfoFactory.Create(queueOrOtherIdentify, ConfigUtil.CreateContainerVirtualPathDic(virtualPath))))
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
            _channel.QueueDeclare(queue: rabbitMessageQueueInfo.Queue, durable: rabbitMessageQueueInfo.Persistent, exclusive: false, autoDelete: rabbitMessageQueueInfo.AutoDelQueue, arguments: null);
            if (rabbitMessageQueueInfo.Qos != null)
            {
                _channel.BasicQos(0, rabbitMessageQueueInfo.Qos.GetValueOrDefault(), false);
            }

            _channel.QueueBind(rabbitMessageQueueInfo.Queue, rabbitMessageQueueInfo.Exchange, rabbitMessageQueueInfo.Queue);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: rabbitMessageQueueInfo.Queue, autoAck: false, consumer: consumer);

            consumer.Received += (model, ea) =>
            {
                // 错误返回信息
                BasicReturnInfo errorReturn = null;

                // 返回给客户端的数据
                byte[] outData = null;

                // 关联ID
                string correlationId = null;
                IBasicProperties props = null;
                IBasicProperties replyProps = null;
                try
                {
                    props = ea.BasicProperties;
                    replyProps = _channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;
                    correlationId = props.CorrelationId;

                    byte[] inData = ea.Body.IsEmpty ? null : ea.Body.ToArray();
                    try
                    {
                        outData = receiveMessageFun(inData);
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorAsync("RpcServer回调业务处理出现异常", ex, typeof(RabbitRpcServer).Name, correlationId);

                        errorReturn = new BasicReturnInfo();
                        errorReturn.SetFailureMsg("业务处理出现异常", ex.Message);

                        outData = Encoding.UTF8.GetBytes(JsonUtil.SerializeIgnoreNull(errorReturn));
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorAsync("RpcServer接收消息处理出现异常", ex, typeof(RabbitRpcServer).Name, correlationId);

                    errorReturn = new BasicReturnInfo();
                    errorReturn.SetFailureMsg("RpcServer接收消息处理出现异常", ex.Message);

                    outData = Encoding.UTF8.GetBytes(JsonUtil.SerializeIgnoreNull(errorReturn));
                }
                finally
                {
                    if (props != null && replyProps != null)
                    {
                        _channel.BasicPublish(exchange: rabbitMessageQueueInfo.Exchange, routingKey: props.ReplyTo, basicProperties: replyProps, body: outData);
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                }
            };
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
        ~RabbitRpcServer()
        {
            Dispose();
        }
    }
}
