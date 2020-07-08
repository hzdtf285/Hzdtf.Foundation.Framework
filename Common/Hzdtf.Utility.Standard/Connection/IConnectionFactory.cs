using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接工厂接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionT">连接类型</typeparam>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public interface IConnectionFactory<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT>
        where ConnectionT : IConnection<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
        where ConnectionWrapInfoT : ConnectionWrapInfo<ConnectionInfoT>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>连接</returns>
        ConnectionT Create();

        /// <summary>
        /// 创建并打开
        /// </summary>
        /// <param name="connectionWrap">连接包装信息</param>
        /// <returns>连接</returns>
        ConnectionT CreateAndOpen(ConnectionWrapInfoT connectionWrap = null);
    }

    /// <summary>
    /// 连接工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IConnectionFactory : IConnectionFactory<IConnection<ConnectionInfo>, ConnectionInfo, ConnectionWrapInfo<ConnectionInfo>>
    {
    }
}
