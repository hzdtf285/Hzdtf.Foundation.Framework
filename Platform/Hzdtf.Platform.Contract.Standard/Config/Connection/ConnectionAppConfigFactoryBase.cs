using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.Connection
{
    /// <summary>
    /// 连接带有应用配置的工厂基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionT">连接类型</typeparam>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public abstract class ConnectionAppConfigFactoryBase<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT> : ConnectionFactoryBase<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT>
        where ConnectionT : IConnection<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
        where ConnectionWrapInfoT : ConnectionWrapInfo<ConnectionInfoT>
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        protected readonly IAppConfiguration appConfig;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        /// <param name="appConfig">应用配置</param>
        public ConnectionAppConfigFactoryBase(ISymmetricalEncryption symmetricalEncryption = null, IAppConfiguration appConfig = null)
            : base(symmetricalEncryption)
        {
            if (appConfig == null)
            {
                appConfig = PlatformTool.AppConfig;
                if (appConfig == null)
                {
                    throw new ArgumentNullException("未注入应用配置对象，请在构造里传入或者设置PlatformTool.AppConfig");
                }
            }
            this.appConfig = appConfig;
        }

        /// <summary>
        /// 根据应用配置名称获取连接字符串
        /// </summary>
        /// <param name="appConfigName">应用配置名称</param>
        /// <returns>连接字符串</returns>
        protected override string GetConnectionStringByAppConfigName(string appConfigName) => appConfig[appConfigName];
    }
}
