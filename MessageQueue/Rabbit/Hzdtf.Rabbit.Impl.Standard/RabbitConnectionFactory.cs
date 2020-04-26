using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Rabbit.Impl.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard
{
    /// <summary>
    /// Rabbit连接工厂
    /// @ 黄振东
    /// </summary>
    public static class RabbitConnectionFactory
    {
        /// <summary>
        /// 创建连接且打开
        /// </summary>
        /// <param name="connectionStringConfigName">连接字符串配置名称</param>
        /// <param name="messageQueueFilePath">消息队列文件路径</param>
        /// <param name="dataContentType">数据内容类型。只支持JSON和XML</param>
        /// <returns>连接</returns>
        public static IMessageQueueConnection CreateAndOpen(string connectionStringConfigName = null, string messageQueueFilePath = null, DataContentType dataContentType = DataContentType.JSON)
        {
            if (!(dataContentType == DataContentType.JSON || dataContentType == DataContentType.XML))
            {
                throw new NotSupportedException("数据内容类型只支持JSON或XML");
            }

            var conn = new RabbitAutoRecoveryConnection();
            if (string.IsNullOrWhiteSpace(connectionStringConfigName))
            {
                conn.Open();
            }
            else
            {
                var connString = PlatformTool.AppConfig[connectionStringConfigName];
                if (ConfigUtil.ConnectionEncryption)
                {
                    connString = DESUtil.Decrypt(connString, PlatformTool.AppConfig["DES:Key"], PlatformTool.AppConfig["DES:IV"]);
                }
                conn.Open(connString);
            }

            if (!string.IsNullOrWhiteSpace(messageQueueFilePath))
            {
                var rabbitConn = conn.ProtoConnection as RabbitConnection;
                var messageQueueInfoFactory = new RabbitMessageQueueInfoConfigFactory();
                rabbitConn.MessageQueueInfoFactory = messageQueueInfoFactory;

                if (dataContentType == DataContentType.JSON)
                {
                    messageQueueInfoFactory.MessageQueueReader = new RabbitMessageQueueJson(messageQueueFilePath);
                }
                else
                {
                    messageQueueInfoFactory.MessageQueueReader = new RabbitMessageQueueXml(messageQueueFilePath);
                }
            }

            return conn;
        }
    }
}
