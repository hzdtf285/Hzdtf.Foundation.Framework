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
                    lock (syncConnection)
                    {
                        if (connection == null)
                        {
                            connection = CreateConnection();
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
        /// 根据配置名称创建一个连接并打开
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <returns>连接</returns>
        public static IMessageQueueConnection CreateConnectionFromConfigName(string configName)
        {
            if (string.IsNullOrWhiteSpace(configName))
            {
                throw new ArgumentNullException("配置名称不能为空");
            }
            if (AppConfig == null)
            {
                throw new NullReferenceException("配置对象为null，请先设置PlatformTool.AppConfig");
            }
            var connStr = AppConfig[configName];
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentNullException($"[{configName}]配置的连接字符串为空");
            }

            return CreateConnectionFromConnString(connStr);
        }

        /// <summary>
        /// 创建一个连接并打开
        /// </summary>
        /// <returns>连接</returns>
        public static IMessageQueueConnection CreateConnection()
        {
            var connection = CreateSimpleConnection();
            connection.Open();

            return connection;
        }

        /// <summary>
        /// 根据连接字符串创建一个连接并打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接</returns>
        public static IMessageQueueConnection CreateConnectionFromConnString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("连接字符串不能为空");
            }
            var connection = CreateSimpleConnection();
            connection.Open(connectionString);

            return connection;
        }

        /// <summary>
        /// 根据信息创建一个连接并打开
        /// </summary>
        /// <param name="connInfo">连接信息</param>
        /// <returns>连接</returns>
        public static IMessageQueueConnection CreateConnectionFromInfo(ConnectionInfo connInfo)
        {
            var connection = CreateSimpleConnection();
            connection.Open(connInfo);

            return connection;
        }

        /// <summary>
        /// 创建一个简单连接
        /// </summary>
        /// <returns>简单连接</returns>
        private static IMessageQueueConnection CreateSimpleConnection()
        {
            string classFullPath = AppConfig["MessageQueue:ConnectionClassFullPath"];
            if (string.IsNullOrWhiteSpace(classFullPath))
            {
                throw new Exception("请在配置文件里配置[MessageQueue:ConnectionClassFullPath]连接执行类路径");
            }

            return ReflectUtil.CreateInstance<IMessageQueueConnection>(classFullPath);
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