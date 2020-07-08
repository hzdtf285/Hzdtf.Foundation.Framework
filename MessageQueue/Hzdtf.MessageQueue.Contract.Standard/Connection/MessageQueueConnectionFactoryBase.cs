using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard.Config.Connection;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Connection
{
    /// <summary>
    /// 消息队列连接工厂基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public abstract class MessageQueueConnectionFactoryBase<ConnectionWrapInfoT> : ConnectionAppConfigFactoryBase<IMessageQueueConnection, ConnectionInfo, ConnectionWrapInfoT>,
        IMessageQueueConnectionFactory<ConnectionWrapInfoT>
        where ConnectionWrapInfoT : ConnectionWrapInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        /// <param name="appConfig">应用配置</param>
        public MessageQueueConnectionFactoryBase(ISymmetricalEncryption symmetricalEncryption = null, IAppConfiguration appConfig = null)
            : base(symmetricalEncryption, appConfig)
        {
        }

        /// <summary>
        /// 应用配置连接是否加密，默认为否
        /// </summary>
        /// <returns>应用配置连接是否加密</returns>
        protected override bool AppConfigConnectionEncryption() => MessageQueueUtil.ConnectionEncrypt(appConfig);
    }
}
