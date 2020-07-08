using Hzdtf.Utility.Standard.Release;
using System;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    public interface IConnection<ConnectionInfoT> : ICloseable, IDisposable
        where ConnectionInfoT : ConnectionInfo
    {
        #region 属性

        /// <summary>
        /// 状态
        /// </summary>
        ConnectionStatusType Status
        {
            get;
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        ConnectionInfoT ConnectionInfo
        {
            get;
        }

        #endregion

        #region 打开关闭

        /// <summary>
        /// 打开
        /// </summary>
        void Open();

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        void Open(string connectionString);

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        void Open(ConnectionInfoT connectionInfo);

        #endregion
    }

    /// <summary>
    /// 连接接口
    /// @ 黄振东
    /// </summary>
    public interface IConnection : IConnection<ConnectionInfo>
    {
    }
}
