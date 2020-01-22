using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Model.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列信息
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueInfo : MessageQueueInfo
    {
        #region 属性与字段

        /// <summary>
        /// 交换机
        /// </summary>
        private string exchange;

        /// <summary>
        /// 交换机
        /// </summary>
        public string Exchange
        {
            get
            { 
                // 如果交换机为空，则依照类型设置对应默认交换机
                if (string.IsNullOrWhiteSpace(exchange))
                {
                    switch (Type)
                    {
                        case RabbitConfigUtil.DIRECT_EXCHANGE_NAME:
                            exchange = RabbitConfigUtil.DEFAULT_DIRECT_EXCHANGE;

                            break;

                        case RabbitConfigUtil.TOPIC_EXCHANGE_NAME:
                            exchange = RabbitConfigUtil.DEFAULT_TOPIC_EXCHANGE;

                            break;

                        case RabbitConfigUtil.FANOUT_EXCHANGE_NAME:
                            exchange = RabbitConfigUtil.DEFAULT_FANOUT_EXCHANGE;

                            break;
                    }
                }

                return exchange;
            }
            set { exchange = value; }
        }

        /// <summary>
        /// 类型（默认直通)
        /// </summary>
        public string Type
        {
            get;
            set;
        } = RabbitConfigUtil.DIRECT_EXCHANGE_NAME;

        /// <summary>
        /// 持久化
        /// </summary>
        public bool Persistent
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 路由键集合
        /// </summary>
        public string[] RoutingKeys
        {
            get;
            set;
        }

        /// <summary>
        /// 自动删除队列
        /// </summary>
        public bool AutoDelQueue
        {
            get;
            set;
        }

        /// <summary>
        /// 服务质量数
        /// </summary>
        public ushort? Qos
        {
            get;
            set;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 将消息队列信息转换为Rabbit消息队列信息
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>Rabbit消息队列信息</returns>
        public static RabbitMessageQueueInfo From(MessageQueueInfo messageQueueInfo)
        {
            if (messageQueueInfo == null)
            {
                return null;
            }

            if (messageQueueInfo is RabbitMessageQueueInfo)
            {
                return messageQueueInfo as RabbitMessageQueueInfo;
            }

            throw new NullReferenceException("消息队列信息不能转换为Rabbit消息队列信息");
        }

        #endregion
    }
}
