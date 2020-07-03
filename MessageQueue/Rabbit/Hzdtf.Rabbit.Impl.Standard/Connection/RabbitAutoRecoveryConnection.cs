using Hzdtf.Logger.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Contract.Standard.Connection;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.ProcessCall;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard.Connection
{
    /// <summary>
    /// Rabbit自动恢复的连接
    /// 创建渠道时如果出现过多渠道的异常或在构造方法里指定最大渠道数，如果超过，则会关闭全部的渠道，然后重新创建
    /// 需要用此恢复功能原生连接必须实现IRabbitChannel接口
    /// @ 黄振东
    /// </summary>
    public class RabbitAutoRecoveryConnection : IMessageQueueConnection
    {
        #region 属性与字段

        /// <summary>
        /// 原生消息连接
        /// </summary>
        public IMessageQueueConnection ProtoConnection
        {
            get;
            set;
        } = new RabbitConnection();

        /// <summary>
        /// 最大渠道数
        /// </summary>
        private readonly int maxChannelCount;

        /// <summary>
        /// 日志
        /// </summary>
        public static ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 状态
        /// </summary>
        public ConnectionStatusType Status
        {
            get => ProtoConnection.Status;
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitAutoRecoveryConnection()
            : this(1000)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxChannelCount">最大渠道数</param>
        public RabbitAutoRecoveryConnection(int maxChannelCount)
        {
            this.maxChannelCount = maxChannelCount;
        }

        #endregion

        #region IConnection 接口

        #region 打开关闭

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            ProtoConnection.Open();
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public void Open(string connectionString)
        {
            ProtoConnection.Open(connectionString);
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        public void Open(ConnectionInfo connectionInfo)
        {
            ProtoConnection.Open(connectionInfo);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            ProtoConnection.Close();

            OnClosed();
        }

        #endregion

        #region 生产者

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(string queueOrOtherIdentify)
        {
            return CreateChannelFromProto<IProducer>(() =>
            {
                return ProtoConnection.CreateProducer(queueOrOtherIdentify);
            });
        }

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(MessageQueueInfo messageQueueInfo)
        {
            return CreateChannelFromProto<IProducer>(() =>
            {
                return ProtoConnection.CreateProducer(messageQueueInfo);
            });
        }

        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>生产者</returns>
        public IProducer CreateProducer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            return CreateChannelFromProto<IProducer>(() =>
            {
                return ProtoConnection.CreateProducer(messageQueueInfoFactory);
            });
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
            return CreateChannelFromProto<IConsumer>(() =>
            {
                return ProtoConnection.CreateConsumer(queueOrOtherIdentify);
            });
        }

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>消费者</returns>
        public IConsumer CreateConsumer(MessageQueueInfo messageQueueInfo)
        {
            return CreateChannelFromProto<IConsumer>(() =>
            {
                return ProtoConnection.CreateConsumer(messageQueueInfo);
            });
        }

        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>消费者</returns>
        public IConsumer CreateConsumer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            return CreateChannelFromProto<IConsumer>(() =>
            {
                return ProtoConnection.CreateConsumer(messageQueueInfoFactory);
            });
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
            return CreateChannelFromProto<IRpcClient>(() =>
            {
                return ProtoConnection.CreateRpcClient(queueOrOtherIdentify);
            });
        }

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>RPC客户端</returns>
        public IRpcClient CreateRpcClient(MessageQueueInfo messageQueueInfo)
        {
            return CreateChannelFromProto<IRpcClient>(() =>
            {
                return ProtoConnection.CreateRpcClient(messageQueueInfo);
            });
        }

        /// <summary>
        /// 创建RPC客户端
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC客户端</returns>
        public IRpcClient CreateRpcClient(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            return CreateChannelFromProto<IRpcClient>(() =>
            {
                return ProtoConnection.CreateRpcClient(messageQueueInfoFactory);
            });
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
            return CreateChannelFromProto<IRpcServer>(() =>
            {
                return ProtoConnection.CreateRpcServer(queueOrOtherIdentify);
            });
        }

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="messageQueueInfo">消息队列信息</param>
        /// <returns>RPC服务端</returns>
        public IRpcServer CreateRpcServer(MessageQueueInfo messageQueueInfo)
        {
            return CreateChannelFromProto<IRpcServer>(() =>
            {
                return ProtoConnection.CreateRpcServer(messageQueueInfo);
            });
        }

        /// <summary>
        /// 创建RPC服务端
        /// </summary>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <returns>RPC服务端</returns>
        public IRpcServer CreateRpcServer(IMessageQueueInfoFactory messageQueueInfoFactory)
        {
            return CreateChannelFromProto<IRpcServer>(() =>
            {
                return ProtoConnection.CreateRpcServer(messageQueueInfoFactory);
            });
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 从原生里创建渠道
        /// </summary>
        /// <typeparam name="T">渠道类型</typeparam>
        /// <param name="func">回调功能</param>
        /// <returns>渠道</returns>
        private T CreateChannelFromProto<T>(Func<T> func)
        {
            IRabbitChannel rabbitChannel = ProtoConnection as IRabbitChannel;

            // 如果当前渠道数大于或等于最大渠道数，则先关闭所有渠道
            if (maxChannelCount != -1 && rabbitChannel.GetChannelCount() >= maxChannelCount)
            {
                Log.Wran(string.Format("未即时关闭的渠道数:{0}已超过最大数:{1}。现执行关闭现所有的渠道后再创建。",
                    typeof(RabbitAutoRecoveryConnection).Name,
                    rabbitChannel.GetChannelCount(), maxChannelCount));
                rabbitChannel.CloseChannels();

                return func();
            }

            try
            {
                return func();
            }
            catch (ChannelAllocationException ex)
            {
                Log.Wran("创建渠道发生渠道过多异常，原因可能是客户端创建渠道，使用完没即时关闭。先关闭现所有的渠道，再重新创建，不影响正常流程",
                    ex, typeof(RabbitAutoRecoveryConnection).Name);

                // 如果是渠道过多异常，则把已有的渠道关闭再创建
                rabbitChannel.CloseChannels();

                return func();
            }
        }

        /// <summary>
        /// 判断原生连接是否实现IRabbitChannel接口
        /// </summary>
        private void ValidateProtoConnIsRabbitChannel()
        {
            if (ProtoConnection == null)
            {
                throw new NullReferenceException("原生连接不能为null");
            }
            if (ProtoConnection is IRabbitChannel)
            {
                return;
            }

            throw new NotImplementedException("原生连接未实现IRabbitChannel接口");
        }

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

        #region IDisposable 接口

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #endregion

        /// <summary>
        /// 析构方法
        /// </summary>
        ~RabbitAutoRecoveryConnection()
        {
            Dispose();
        }
    }
}
