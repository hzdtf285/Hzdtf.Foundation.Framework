using Hzdtf.Logger.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Release;
using Hzdtf.Utility.Standard.Utils;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard.Core
{
    /// <summary>
    /// Rabbit核心基类
    /// @ 黄振东
    /// </summary>
    public abstract class RabbitCoreBase : ICloseable, IDisposable
    {
        #region 属性与字段

        /// <summary>
        /// 渠道
        /// </summary>
        protected readonly IModel channel;

        /// <summary>
        /// Rabbit消息队列信息
        /// </summary>
        protected readonly RabbitMessageQueueInfo rabbitMessageQueueInfo;

        /// <summary>
        /// 基本属性集合
        /// </summary>
        protected IBasicProperties basicProperties;

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// 初始化各个对象以便就绪
        /// 只初始化交换机与基本属性，队列定义请重写Init方法进行操作
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="rabbitMessageQueueInfo">Rabbit消息队列信息</param>
        public RabbitCoreBase(IModel channel, RabbitMessageQueueInfo rabbitMessageQueueInfo)
        {
            ValidateUtil.ValidateNull(channel, "渠道");
            ValidateUtil.ValidateNull(rabbitMessageQueueInfo, "消息队列信息");

            this.channel = channel;
            this.rabbitMessageQueueInfo = rabbitMessageQueueInfo;

            channel.ExchangeDeclare(rabbitMessageQueueInfo.Exchange, rabbitMessageQueueInfo.Type, rabbitMessageQueueInfo.Persistent);
            if (rabbitMessageQueueInfo.Qos != null)
            {
                channel.BasicQos(0, rabbitMessageQueueInfo.Qos.GetValueOrDefault(), false);
            }

            basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = rabbitMessageQueueInfo.Persistent;

            Init();
        }

        /// <summary>
        /// 构造方法
        /// 初始化各个对象以便就绪
        /// </summary>
        /// <param name="channel">渠道</param>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="messageQueueInfoFactory">消息队列信息工厂</param>
        /// <param name="virtualPath">虚拟路径</param>
        public RabbitCoreBase(IModel channel, string queueOrOtherIdentify, IMessageQueueInfoFactory messageQueueInfoFactory, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
            : this(channel, RabbitMessageQueueInfo.From(messageQueueInfoFactory.Create(queueOrOtherIdentify, ConfigUtil.CreateContainerVirtualPathDic(virtualPath))))
        {
        }

        #endregion

        #region ICloseable 接口

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        { 
            if (channel != null && channel.IsOpen)
            {               
                channel.Close();
                channel.Dispose();
            }

            OnClosed();
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
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
        ~RabbitCoreBase()
        {
            Dispose();
        }
    }
}
