using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard
{
    /// <summary>
    /// 配置辅助类
    /// @ 黄振东
    /// </summary>
    public static class ConfigUtil
    {
        /// <summary>
        /// 获取虚拟路径，如果传入的为空或未找到key=virtualPath，则默认返回"/"
        /// </summary>
        /// <param name="extend">扩展</param>
        /// <returns>虚拟路径</returns>
        public static string GetVirtualPath(this IDictionary<string, string> extend)
        {
            return extend.GetValue(RabbitConnectionInfo.VIRTUAL_PATH_NAME, RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH);
        }

        /// <summary>
        /// 创建一个包含虚拟路径的字典
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>字典</returns>
        public static IDictionary<string, string> CreateContainerVirtualPathDic(string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            return new Dictionary<string, string>()
            {
                { RabbitConnectionInfo.VIRTUAL_PATH_NAME, virtualPath }
            };
        }

        /// <summary>
        /// 设置消息队列读取
        /// </summary>
        /// <param name="conn">消息队列连接</param>
        /// <param name="reader">消息队列读取</param>
        public static void SetMessageQueueReader(this IMessageQueueConnection conn, IRabbitMessageQueueReader reader)
        {
            if (conn == null)
            {
                return;
            }

            RabbitConnection rabbitConn = null;
            if (conn is RabbitAutoRecoveryConnection)
            {
                var autoRabbitConn = conn as RabbitAutoRecoveryConnection;
                if (autoRabbitConn.ProtoConnection == null)
                {
                    autoRabbitConn.ProtoConnection = new RabbitConnection();
                }
                else if (autoRabbitConn.ProtoConnection is RabbitConnection)
                {
                    rabbitConn = autoRabbitConn.ProtoConnection as RabbitConnection;
                }
                else
                {
                    throw new NotSupportedException("RabbitAutoRecoveryConnection.ProtoConnection不是RabbitConnection");
                }
            }
            else if (conn is RabbitConnection)
            {
                rabbitConn = conn as RabbitConnection;
            }
            else
            {
                throw new NotSupportedException("不支持的连接类型，必须是RabbitConnection");
            }

            RabbitMessageQueueInfoConfigFactory msgQueueConfigFactoy = null;
            if (rabbitConn.MessageQueueInfoFactory == null)
            {
                msgQueueConfigFactoy = new RabbitMessageQueueInfoConfigFactory();
                rabbitConn.MessageQueueInfoFactory = msgQueueConfigFactoy;
            }
            else if (rabbitConn.MessageQueueInfoFactory is RabbitMessageQueueInfoConfigFactory)
            {
                msgQueueConfigFactoy = rabbitConn.MessageQueueInfoFactory as RabbitMessageQueueInfoConfigFactory;
            }
            else
            {
                throw new NotSupportedException("消息队列信息工厂不支持的类型，必须是RabbitMessageQueueInfoConfigFactory");
            }

            msgQueueConfigFactoy.MessageQueueReader = reader;
        }
    }
}
