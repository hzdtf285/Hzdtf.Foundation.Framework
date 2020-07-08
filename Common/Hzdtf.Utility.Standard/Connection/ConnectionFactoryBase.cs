using Hzdtf.Utility.Standard.Safety;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接工厂基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionT">连接类型</typeparam>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public abstract class ConnectionFactoryBase<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT> : IConnectionFactory<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT>
        where ConnectionT : IConnection<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
        where ConnectionWrapInfoT : ConnectionWrapInfo<ConnectionInfoT>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>连接</returns>
        public abstract ConnectionT Create();

        /// <summary>
        /// 加密
        /// </summary>
        protected readonly ISymmetricalEncryption symmetricalEncryption;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        public ConnectionFactoryBase(ISymmetricalEncryption symmetricalEncryption = null)
        {
            if (symmetricalEncryption == null)
            {
                this.symmetricalEncryption = new DES();
            }
            else
            {
                this.symmetricalEncryption = symmetricalEncryption;
            }
        }

        /// <summary>
        /// 创建并打开
        /// </summary>
        /// <param name="connectionWrap">连接包装信息</param>
        /// <returns>连接</returns>
        public virtual ConnectionT CreateAndOpen(ConnectionWrapInfoT connectionWrap = null)
        {
            ConnectionT conn = default(ConnectionT);
            if (connectionWrap == null)
            {
                return Create();
            }
            if (connectionWrap.DefaultConnection != null && connectionWrap.DefaultConnection is ConnectionT)
            {
                conn = (ConnectionT)connectionWrap.DefaultConnection;
            }
            else
            {
                conn = Create();
            }

            if (connectionWrap.ConnectionInfo != null)
            {
                conn.Open(connectionWrap.ConnectionInfo);

                return conn;
            }

            if (!string.IsNullOrWhiteSpace(connectionWrap.ConnectionString))
            {
                conn.Open(connectionWrap.ConnectionString);

                return conn;
            }

            if (!string.IsNullOrWhiteSpace(connectionWrap.ConnectionStringAppConfigName))
            {
                var connString = GetConnectionStringByAppConfigName(connectionWrap.ConnectionStringAppConfigName);
                if (!string.IsNullOrWhiteSpace(connString))
                {
                    if (AppConfigConnectionEncryption())
                    {
                        connString = symmetricalEncryption.Decrypt(connString);
                    }

                    conn.Open(connString);

                    return conn;
                }
            }

            if (NotEqualExecOpen(conn, connectionWrap))
            {
                return conn;
            }

            conn.Open();

            return conn;
        }

        /// <summary>
        /// 根据应用配置名称获取连接字符串
        /// </summary>
        /// <param name="appConfigName">应用配置名称</param>
        /// <returns>连接字符串</returns>
        protected abstract string GetConnectionStringByAppConfigName(string appConfigName);

        /// <summary>
        /// 应用配置连接是否加密，默认为否
        /// </summary>
        /// <returns>应用配置连接是否加密</returns>
        protected virtual bool AppConfigConnectionEncryption() => false;

        /// <summary>
        /// 未匹配上执行打开连接
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="connectionWrap">连接包装信息</param>
        /// <returns>是否已打开</returns>
        protected virtual bool NotEqualExecOpen(ConnectionT conn, ConnectionWrapInfoT connectionWrap) => false;
    }

    /// <summary>
    /// 连接工厂基类
    /// @ 黄振东
    /// </summary>
    public abstract class ConnectionFactoryBase : ConnectionFactoryBase<IConnection<ConnectionInfo>, ConnectionInfo, ConnectionWrapInfo<ConnectionInfo>>
    {
    }
}
