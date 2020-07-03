using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Data;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Rabbit.Model.Standard.Utils;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit生产者
    /// @ 黄振东
    /// </summary>
    public class RabbitProducer : RabbitCoreBase, IProducer
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

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        public RabbitProducer(IModel channel, IMessageQueueInfoFactory messageQueueInfoFactory)
            : base(channel, messageQueueInfoFactory)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitProducer(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
            : base(channel, rabbitMessageQueueInfo)
        {
        }

        #endregion

        #region IProducer 接口 

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布的路由规则</param>
        public void Publish(string message, string publishRoutingKey = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Publish(Encoding.UTF8.GetBytes(message), publishRoutingKey);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布的路由规则</param>
        public void Publish(object message, string publishRoutingKey = null)
        {
            if (message == null)
            {
                return;
            }

            Publish(BytesSerialization.Serialize(message), publishRoutingKey);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="publishRoutingKey">发布路由键</param>
        public void Publish(byte[] message, string publishRoutingKey = null)
        {
            if (message.IsNullOrLength0())
            {
                return;
            }

            string key = RabbitUtil.GetPublishKey(publishRoutingKey, rabbitMessageQueueInfo.RoutingKeys);
            string logMsg = string.Format("给key:{0},交换机:{1} 发送消息", key, rabbitMessageQueueInfo.Exchange);
            Log.DebugAsync(logMsg, null, typeof(RabbitProducer).Name, rabbitMessageQueueInfo.Exchange, key);

            channel.BasicPublish(rabbitMessageQueueInfo.Exchange, key,  basicProperties, message);
        }

        #endregion
    }
}
