using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Rabbit.Contract.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard.Core;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Utility.Standard.Release;
using Hzdtf.Utility.Standard.Safety;
using Hzdtf.Utility.Standard.Utils;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard.Connection
{
    /// <summary>
    /// Rabbit连接
    /// @ 黄振东
    /// </summary>
    public class RabbitConnection : ConnectionBase, IMessageQueueConnection, IRabbitChannel
    {
        #region 属性与字段

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 连接
        /// </summary>
        private RabbitMQ.Client.IConnection connection;

        /// <summary>
        /// 渠道列表
        /// </summary>
        private readonly IList<IModel> channels = new List<IModel>();

        /// <summary>
        /// 状态
        /// </summary>
        public override ConnectionStatusType Status
        {
            get
            {
                if (connection != null)
                {
                    return connection.IsOpen ? ConnectionStatusType.OPENED: ConnectionStatusType.CLOSED;
                }

                return ConnectionStatusType.CLOSED;
            }
        }

        /// <summary>
        /// 消息队列信息工厂
        /// </summary>
        public IMessageQueueInfoFactory MessageQueueInfoFactory
        {
            get;
            set;
        }

        #endregion

        #region IMessageQueueConnection 接口

        #region 生产者

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(string queueOrOtherIdentify)
        {
            return CreateProducer(ValidateMessageQueueInfoFromConfig(queueOrOtherIdentify));
        }

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(MessageQueueInfo messageQueueInfo)
        {
            ValidateUtil.ValidateNull(messageQueueInfo, "Rabbit消息队列信息");
            ValidateConnection();

            IProducer producer = new RabbitProducer(CreateChannel(), RabbitMessageQueueInfo.From(messageQueueInfo));
            AddClosedEventHandler(producer);

            return producer;
        }

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            ValidateUtil.ValidateNull(messageQueueInfoFactory, "消息队列信息工厂");
            ValidateConnection();

            IProducer producer = new RabbitProducer(CreateChannel(), messageQueueInfoFactory);
            AddClosedEventHandler(producer);

            return producer;
        }

        #endregion

        #region 消费者

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消费者</returns>
        public IConsumer CreateConsumer(string queueOrOtherIdentify)
        {
            return CreateConsumer(ValidateMessageQueueInfoFromConfig(queueOrOtherIdentify));
        }

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>消费者</returns>
        public IConsumer CreateConsumer(MessageQueueInfo messageQueueInfo)
        {
            ValidateUtil.ValidateNull(messageQueueInfo, "Rabbit消息队列信息");
            ValidateConnection();

            IConsumer consumer = new RabbitConsumer(CreateChannel(), RabbitMessageQueueInfo.From(messageQueueInfo));
            AddClosedEventHandler(consumer);

            return consumer;
        }

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>消费者</returns>
        public IConsumer CreateConsumer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            ValidateUtil.ValidateNull(messageQueueInfoFactory, "消息队列信息工厂");
            ValidateConnection();

            IConsumer consumer = new RabbitConsumer(CreateChannel(), messageQueueInfoFactory);
            AddClosedEventHandler(consumer);

            return consumer;
        }

        #endregion

        #region RPC客户端

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>RPC客户端</returns>
        public IRpcClient CreateRpcClient(string queueOrOtherIdentify)
        {
            return CreateRpcClient(ValidateMessageQueueInfoFromConfig(queueOrOtherIdentify));
        }

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>RPC客户端</returns>
        public IRpcClient CreateRpcClient(MessageQueueInfo messageQueueInfo)
        {
            ValidateUtil.ValidateNull(messageQueueInfo, "Rabbit消息队列信息");
            ValidateConnection();

            IRpcClient rpcClient = new RabbitRpcClient(CreateChannel(), RabbitMessageQueueInfo.From(messageQueueInfo));
            AddClosedEventHandler(rpcClient);

            return rpcClient;
        }

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC客户端</returns>
        public IRpcClient CreateRpcClient(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            ValidateUtil.ValidateNull(messageQueueInfoFactory, "消息队列信息工厂");
            ValidateConnection();

            IRpcClient rpcClient = new RabbitRpcClient(CreateChannel(), messageQueueInfoFactory);
            AddClosedEventHandler(rpcClient);

            return rpcClient;
        }

        #endregion

        #region RPC服务端

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>RPC服务端</returns>
        public IRpcServer CreateRpcServer(string queueOrOtherIdentify)
        {
            return CreateRpcServer(ValidateMessageQueueInfoFromConfig(queueOrOtherIdentify));
        }

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>RPC服务端</returns>
        public IRpcServer CreateRpcServer(MessageQueueInfo messageQueueInfo)
        {
            ValidateUtil.ValidateNull(messageQueueInfo, "Rabbit消息队列信息");
            ValidateConnection();

            IRpcServer rpcServer = new RabbitRpcServer(CreateChannel(), RabbitMessageQueueInfo.From(messageQueueInfo));
            AddClosedEventHandler(rpcServer);

            return rpcServer;
        }

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC服务端</returns>
        public IRpcServer CreateRpcServer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            ValidateUtil.ValidateNull(messageQueueInfoFactory, "消息队列信息工厂");
            ValidateConnection();

            IRpcServer rpcServer = new RabbitRpcServer(CreateChannel(), messageQueueInfoFactory);
            AddClosedEventHandler(rpcServer);

            return rpcServer;
        }

        #endregion

        #endregion

        #region IRabbitChannel 接口

        /// <summary>
        /// 获取渠道数
        /// </summary>
        /// <returns>渠道数</returns>
        public int GetChannelCount()
        {
            return channels.Count;
        }

        /// <summary>
        /// 关闭渠道集合
        /// </summary>
        /// <param name="topCount">前几个要关闭的，如果为-1则表示全部</param>
        public void CloseChannels(int topCount = -1)
        {
            if (channels.Count == 0)
            {
                return;
            }
            if (topCount == -1)
            {
                topCount = channels.Count;
            }
            else if (topCount > channels.Count)
            {
                topCount = channels.Count;
            }

            for (int i = 0; i < topCount; i++)
            {
                IModel channel = channels[0];
                
                if (channel.IsOpen)
                {
                    channel.Close();
                    channel.Dispose();
                }
                channels.RemoveAt(0);
            }
        }

        #endregion

        #region 重写父类的方法

        #region 打开关闭

        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            if (channels.Count > 0)
            {
                foreach (IModel channel in channels)
                {
                    if (channel.IsOpen)
                    {
                        channel.Close();
                        channel.Dispose();
                    }
                }

                channels.Clear();
            }

            if (Status == ConnectionStatusType.OPENED)
            {
                connection.Close();
                connection.Dispose();
            }

            connection = null;

            OnClosed();
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 获取默认的连接字符串
        /// </summary>
        /// <returns>默认的连接字符串</returns>
        protected override string GetDefaultConnectionString()
        {
            var connString = AppConfig["MessageQueue:RabbitConnectionString"];
            if (string.IsNullOrWhiteSpace(connString))
            {
                return connString;
            }

            return ConfigUtil.ConnectionEncryption ? DESUtil.Decrypt(connString, AppConfig["DES:Key"], AppConfig["DES:IV"]) : connString;
        }

        /// <summary>
        /// 执行打开
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        protected override void ExecOpen(ConnectionInfo connectionInfo)
        {
            connection = GetConnectionFactory(ValidateOpenParams(connectionInfo)).CreateConnection();
        }

        /// <summary>
        /// 获取默认的连接字符串解析器
        /// </summary>
        /// <returns>默认的连接字符串解析器</returns>
        protected override IConnectionStringParse GetDefaultConnectionStringParse()
        {
            return new RabbitConnectionStringParse();
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证打开参数
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        /// <returns>Rabbit连接信息</returns>
        private RabbitConnectionInfo ValidateOpenParams(ConnectionInfo connectionInfo)
        {
            ValidateUtil.ValidateNull(connectionInfo, "连接信息");
            RabbitConnectionInfo rabbitConnectionInfo = RabbitConnectionInfo.From(connectionInfo);
            if (connection != null && connection.IsOpen)
            {
                throw new Exception("已打开连接，不允许重复打开。如需打开不同连接请先关闭原有连接");
            }

            return rabbitConnectionInfo;
        }

        /// <summary>
        /// 根据Rabbit连接信息获取连接工厂
        /// </summary>
        /// <param name="rabbitConnectionInfo">Rabbit连接信息</param>
        /// <returns>连接工厂</returns>
        private ConnectionFactory GetConnectionFactory(RabbitConnectionInfo rabbitConnectionInfo)
        {
            return new ConnectionFactory()
            {
                HostName = rabbitConnectionInfo.Host,
                VirtualHost = rabbitConnectionInfo.VirtualPath,
                Password = rabbitConnectionInfo.Password,
                UserName = rabbitConnectionInfo.User,
                Port = rabbitConnectionInfo.Port,
                AutomaticRecoveryEnabled = rabbitConnectionInfo.AutoRecovery,
                RequestedHeartbeat = rabbitConnectionInfo.Heartbeat
            };
        }

        /// <summary>
        /// 验证连接，如果为null或未打开则抛出异常
        /// </summary>
        private void ValidateConnection()
        {
            if (connection == null || !connection.IsOpen)
            {
                throw new Exception("请先打开连接");
            }
        }

        /// <summary>
        /// 创建渠道并添加到渠道列表里
        /// </summary>
        /// <returns>渠道</returns>
        private IModel CreateChannel()
        {
            IModel channel = connection.CreateModel();
            channels.Add(channel);            

            return channel;
        }

        /// <summary>
        /// 从配置里验证消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>Rabbit消息队列信息</returns>
        private RabbitMessageQueueInfo ValidateMessageQueueInfoFromConfig(string queueOrOtherIdentify)
        {
            if (MessageQueueInfoFactory == null)
            {
                MessageQueueInfoFactory = new RabbitMessageQueueInfoConfigFactory();
            }

            RabbitMessageQueueInfo rabbitMessageQueueInfo = MessageQueueInfoFactory.Create(queueOrOtherIdentify) as RabbitMessageQueueInfo;
            ValidateUtil.ValidateNull2(rabbitMessageQueueInfo, string.Format("队列或其他标识[{0}]在配置里不存在", queueOrOtherIdentify));            

            return rabbitMessageQueueInfo;
        }
        
        /// <summary>
        /// 为渠道添加关闭后事件处理
        /// </summary>
        /// <param name="channel">渠道</param>
        private void AddClosedEventHandler(ICloseable channel)
        {
            channel.Closed += Channel_Closed;
        }

        /// <summary>
        /// 渠道关闭后
        /// </summary>
        /// <param name="o">引发对象</param>
        /// <param name="e">对象事件参数</param>
        private void Channel_Closed(object o, DataEventArgs e)
        {
            if (o != null && o is ICloseable)
            {
                ((ICloseable)o).Closed -= Channel_Closed;
            }
            if (e == null || e.Data == null)
            {
                return;
            }

            if (e.Data is IModel)
            {
                IModel channel = e.Data as IModel;
                if (channels.Contains(channel))
                {
                    if (channel.IsOpen)
                    {
                        channel.Close();
                    }

                    channels.Remove(channel);
                }
            }
        }

        #endregion

        /// <summary>
        /// 析构方法
        /// </summary>
        ~RabbitConnection()
        {
            Dispose();
        }
    }
}
