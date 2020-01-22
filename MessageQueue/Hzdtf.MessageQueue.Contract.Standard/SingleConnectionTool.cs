using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard
{
    /// <summary>
    /// 单连接工具
    /// 使用单例连接是为了避免创建连接消耗太多资源，如果非可不使用此工具
    /// 在程序退出前或不需要使用消息队列时，要执行关闭方法
    /// 使用之前必须指定CreateConnection或者指定ConnectionClassFullPath（在配置文件（MessageQueue:ConnectionClassFullPath）里指定也可），最好在程序启动时创建，避免重复创建
    /// @ 黄振东
    /// </summary>
    public static class SingleConnectionTool
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        public static IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 连接
        /// </summary>
        private static IMessageQueueConnection connection;

        /// <summary>
        /// 同步连接
        /// </summary>
        private static readonly object syncConnection = new object();

        /// <summary>
        /// 可用的连接且是打开状态
        /// </summary>
        public static IMessageQueueConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    string classFullPath = AppConfig["MessageQueue:ConnectionClassFullPath"];
                    IMessageQueueConnection messageQueueConnection = null;
                    if (!string.IsNullOrWhiteSpace(classFullPath))
                    {
                        messageQueueConnection = ReflectUtil.CreateInstance<IMessageQueueConnection>(classFullPath);
                    }
                    lock (syncConnection)
                    {
                        if (connection == null)
                        {
                            connection = messageQueueConnection;
                            connection.Open();
                        }
                    }
                }
                else
                {
                    if (connection.Status == ConnectionStatusType.OPENED)
                    {
                        return connection;
                    }
                    else
                    {
                        connection.Open();
                    }
                }

                return connection;
            }
            set
            {
                connection = value;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Close()
        {
            if (connection != null && connection.Status == ConnectionStatusType.OPENED)
            {
                connection.Close();
            }

            connection = null;
        }
    }
}