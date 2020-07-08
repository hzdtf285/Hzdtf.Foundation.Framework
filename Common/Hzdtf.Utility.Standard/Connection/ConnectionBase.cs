using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    public abstract class ConnectionBase<ConnectionInfoT> : IConnection<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
    {
        #region 属性与字段

        /// <summary>
        /// 连接字符串解析器
        /// </summary>
        public IConnectionStringParse<ConnectionInfoT> ConnectionStringParse
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public abstract ConnectionStatusType Status
        {
            get;
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        protected ConnectionInfoT connectionInfo;

        /// <summary>
        /// 连接信息
        /// </summary>
        public ConnectionInfoT ConnectionInfo
        {
            get => connectionInfo;
        }

        #endregion

        #region IConnection 接口

        #region 打开关闭

        /// <summary>
        /// 打开
        /// </summary>
        public virtual void Open()
        {
            Open(GetDefaultConnectionString());
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public virtual void Open(string connectionString)
        {
            ValidateUtil.ValidateNullOrEmpty(connectionString, "连接字符串");

            InitConnectionStringParse();
            Open(ConnectionStringParse.Parse(connectionString));
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionModel">连接模型</param>
        public virtual void Open(ConnectionInfoT connectionModel)
        {
            ValidateOpenParams(connectionModel);
            ExecOpen(connectionModel);

            connectionInfo = connectionModel;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            OnClosed(ExecClose());

            connectionInfo = null;
        }

        #endregion

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取默认的连接字符串
        /// </summary>
        /// <returns>默认的连接字符串</returns>
        protected abstract string GetDefaultConnectionString();

        /// <summary>
        /// 执行打开
        /// </summary>
        /// <param name="connectionModel">连接模型</param>
        protected abstract void ExecOpen(ConnectionInfoT connectionModel);

        /// <summary>
        /// 执行关闭
        /// </summary>
        /// <returns>事件数据</returns>
        protected abstract object ExecClose();

        #endregion

        #region 虚方法

        /// <summary>
        /// 获取默认的连接字符串解析器
        /// </summary>
        /// <returns>默认的连接字符串解析器</returns>
        protected virtual IConnectionStringParse<ConnectionInfoT> GetDefaultConnectionStringParse()
        {
            return null;
        }

        /// <summary>
        /// 验证其他打开参数
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        protected virtual void ValidateOtherOpenParams(ConnectionInfoT connectionInfo) { }

        #endregion

        #region 初始化对象

        /// <summary>
        /// 初始化连接字符串解析器
        /// </summary>
        private void InitConnectionStringParse()
        {
            if (ConnectionStringParse == null)
            {
                ConnectionStringParse = GetDefaultConnectionStringParse();
            }
        }

        #endregion

        #region 受保护的方法

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

        /// <summary>
        /// 验证打开参数
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        protected void ValidateOpenParams(ConnectionInfoT connectionInfo)
        {
            if (Status == ConnectionStatusType.OPENED)
            {
                throw new Exception("已打开连接，不允许重复打开。如需打开不同连接请先关闭原有连接");
            }
            ValidateUtil.ValidateNull(connectionInfo, "连接信息");
            if (string.IsNullOrWhiteSpace(connectionInfo.Host))
            {
                throw new ArgumentNullException("主机不能为空");
            }
            if (connectionInfo.Port < 1)
            {
                throw new ArgumentNullException("端口必须大于0");
            }

            ValidateOtherOpenParams(connectionInfo);
        }

        #endregion

        #region IDisposable 接口

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            Close();
        }

        #endregion
    }

    /// <summary>
    /// 连接基类
    /// @ 黄振东
    /// </summary>
    public abstract class ConnectionBase : ConnectionBase<ConnectionInfo>, IConnection
    {
    }
}
