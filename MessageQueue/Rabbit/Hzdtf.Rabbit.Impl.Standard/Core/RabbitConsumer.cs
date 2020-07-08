using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.Rabbit.Model.Standard.Connection;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit消费者
    /// @ 黄振东
    /// </summary>
    public class RabbitConsumer : RabbitCoreBase, IConsumer
    {
        #region 属性与字段

        /// <summary>
        /// 字节数组序列化，默认为JSON序列化
        /// </summary>
        public IBytesSerialization BytesSerialization
        {
            get;
            set;
        } = new JsonBytesSerialization();

        /// <summary>
        /// 业务异常生产者
        /// </summary>
        public IProducer BusinessExceptionProducer
        {
            get;
            set;
        }

        /// <summary>
        /// 业务异常生产者路由Key
        /// </summary>
        private string businessExceptionProducerRouteKey;

        /// <summary>
        /// 业务异常生产者路由Key
        /// </summary>
        private string BusinessExceptionProducerRouteKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(businessExceptionProducerRouteKey))
                {
                    businessExceptionProducerRouteKey = AppConfig["MessageQueue:Publish:BusinessExceptionKey"];
                }

                return businessExceptionProducerRouteKey;
            }
        }

        /// <summary>
        /// 业务异常返回应答，默认为false
        /// </summary>
        public bool BusinessExceptionReturnAck
        {
            get;
            set;
        }

        #endregion

        #region 初始化       

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitConsumer(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
            : base(channel, rabbitMessageQueueInfo)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <param name="virtualPath">虚拟路径</param>
        public RabbitConsumer(IModel channel, string queueOrOtherIdentify, IMessageQueueInfoFactory messageQueueInfoFactory, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
            : base(channel, queueOrOtherIdentify, messageQueueInfoFactory, virtualPath)
        {
        }

        #endregion

        #region IConsumer 接口

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        public void Subscribe(Func<string, bool> receiveMessageFun, bool isAutoAck = false)
        {
            Subscribe((byte[] x) =>
            {
                if (receiveMessageFun != null)
                {
                    string msg = null;
                    try
                    {
                        msg = Encoding.UTF8.GetString(x);
                    }
                    catch(Exception ex)
                    {
                        string logMsg = string.Format("输入参数isAutoAck:{0},Encoding.UTF8.GetString发生异常,返回应答:true,:队列:{1}", isAutoAck, rabbitMessageQueueInfo.Queue);
                        Log.ErrorAsync(logMsg, ex, tags: GetLogTags(rabbitMessageQueueInfo));

                        return true;
                    }

                    try
                    {
                        return receiveMessageFun(msg);
                    }
                    catch (Exception ex)
                    {
                        var isAck = PublishExceptionQueue(ex, msg);

                        string logMsg = string.Format("输入参数isAutoAck:{0},业务处理发生异常(返回应答为{1}):队列:{2}", isAutoAck, isAck, rabbitMessageQueueInfo.Queue);
                        Log.ErrorAsync(logMsg, ex, tags: GetLogTags(rabbitMessageQueueInfo));

                        return isAck;
                    }
                }

                return true;
            }, isAutoAck);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T">接收类型</typeparam>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        public void Subscribe<T>(Func<T, bool> receiveMessageFun, bool isAutoAck = false)
        {
            Subscribe((byte[] x) =>
            {
                if (receiveMessageFun != null)
                {
                    T data = default(T);
                    try
                    {
                        data = BytesSerialization.Deserialize<T>(x);
                    }
                    catch (Exception ex)
                    {
                        string logMsg = string.Format("输入参数isAutoAck:{0},BytesSerialization.Deserialize发生异常(返回应答为true)，认为是不符合业务规范的数据，应删除消息。队列:{1}",
                            isAutoAck, rabbitMessageQueueInfo.Queue);
                        Log.ErrorAsync(logMsg, ex, tags: GetLogTags(rabbitMessageQueueInfo));

                        // 反序列异常则返回true
                        return true;
                    }

                    try
                    {
                        return receiveMessageFun(data);
                    }
                    catch (Exception ex)
                    {
                        var isAck = PublishExceptionQueue(ex, data);

                        string logMsg = string.Format("输入参数isAutoAck:{0},业务处理发生异常(返回应答为{1})，队列:{2}",
                            isAutoAck, isAck, rabbitMessageQueueInfo.Queue);
                        Log.ErrorAsync(logMsg, ex, tags: GetLogTags(rabbitMessageQueueInfo));

                        return isAck;
                    }
                }

                return true;
            }, isAutoAck);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        /// <param name="isAutoAck">是否自动应答，如果为否，则需要在回调里返回true</param>
        public void Subscribe(Func<byte[], bool> receiveMessageFun, bool isAutoAck = false)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (o, e) =>
            {
                bool isAck = true;
                if (receiveMessageFun != null && !e.Body.IsEmpty)
                {
                    string logMsg = string.Format("接收到消息,队列:{0}", rabbitMessageQueueInfo.Queue);
                    Log.DebugAsync(logMsg, null, tags: GetLogTags(rabbitMessageQueueInfo));

                    try
                    {
                        isAck = receiveMessageFun(e.Body.ToArray());
                    }
                    catch (Exception ex)
                    {
                        isAck = PublishExceptionQueue(ex, "字节数组数据");

                        logMsg = string.Format("输入参数isAutoAck:{0},业务处理发生异常(返回应答为{1}):队列:{2}", isAutoAck, isAck, rabbitMessageQueueInfo.Queue);
                        Log.ErrorAsync(logMsg, ex, tags: GetLogTags(rabbitMessageQueueInfo));
                    }
                }

                // 如果自动回答，则什么都不用干
                if (isAutoAck)
                {
                    return;
                }

                // 如果业务端返回应答，则返回MQ已成功处理，否则返回未处理成功，需要将该消息放回队列进行重试
                if (isAck)
                {
                    channel.BasicAck(e.DeliveryTag, false);
                }
                else
                {
                    channel.BasicNack(e.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(rabbitMessageQueueInfo.Queue, isAutoAck, consumer);
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

        #region 受保护的方法

        /// <summary>
        /// 推送异常队列
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="queueMessage">队列消息</param>
        /// <param name="desc">描述</param>
        /// <returns>是否推送成功</returns>
        protected bool PublishExceptionQueue(Exception ex, object queueMessage, string desc = null)
        {
            if (BusinessExceptionProducer == null)
            {
                return BusinessExceptionReturnAck;
            }

            string queueMessageJson = null;
            if (queueMessage != null)
            {
                queueMessageJson = JsonUtil.SerializeIgnoreNull(queueMessage);
            }

            var busEx = new BusinessExceptionInfo()
            {
                Time = DateTime.Now,
                ServiceName = AppConfig["MessageQueue:Consumer:ServiceName"],
                ExceptionString = ex.ToString(),
                ExceptionMessage = ex.Message,
                Exchange = rabbitMessageQueueInfo.Exchange,
                Queue = rabbitMessageQueueInfo.Queue,
                QueueMessageJsonString = queueMessageJson,     
                Desc = desc,
                ServerMachineName = Environment.MachineName,
                ServerIP = NetworkUtil.LocalIP
            };

            BusinessExceptionProducer.Publish(busEx, BusinessExceptionProducerRouteKey);

            return true;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取日志标签集合
        /// </summary>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        /// <returns>日志标签集合</returns>
        private string[] GetLogTags(RabbitMessageQueueInfo rabbitMessageQueueInfo)
        {
            return new string[] { typeof(RabbitConsumer).Name, rabbitMessageQueueInfo.Exchange, rabbitMessageQueueInfo.Queue };
        }

        #endregion
    }
}
