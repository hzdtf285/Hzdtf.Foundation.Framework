using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Safety;

namespace Hzdtf.Rabbit.Impl.Standard.Connection
{
    /// <summary>
    /// Rabbit连接工厂
    /// @ 黄振东
    /// </summary>
    public class RabbitConnectionFactory : MessageQueueConnectionFactoryBase<ConnectionWrapInfo>, IMessageQueueConnectionFactory
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        /// <param name="appConfig">应用配置</param>
        public RabbitConnectionFactory(ISymmetricalEncryption symmetricalEncryption = null, IAppConfiguration appConfig = null)
            : base(symmetricalEncryption, appConfig)
        {
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>连接</returns>
        public override IMessageQueueConnection Create() => new RabbitAutoRecoveryConnection();
    }
}
