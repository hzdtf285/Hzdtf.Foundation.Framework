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
    public abstract class ConnectionBase : IConnection
    {
        #region 属性与字段

        /// <summary>
        /// 连接字符串解析器
        /// </summary>
        public IConnectionStringParse ConnectionStringParse
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

        #endregion

        #region IConnection 接口

        #region 打开关闭

        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            Open(GetDefaultConnectionString());
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public void Open(string connectionString)
        {
            ValidateUtil.ValidateNullOrEmpty(connectionString, "连接字符串");

            InitConnectionStringParse();
            Open(ConnectionStringParse.Parse(connectionString));
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionModel">连接模型</param>
        public void Open(ConnectionInfo connectionModel)
        {
            ValidateUtil.ValidateNull(ConnectionStringParse, "连接模型");

            ExecOpen(connectionModel);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public abstract void Close();

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
        protected abstract void ExecOpen(ConnectionInfo connectionModel);

        #endregion

        #region 虚方法

        /// <summary>
        /// 获取默认的连接字符串解析器
        /// </summary>
        /// <returns>默认的连接字符串解析器</returns>
        protected virtual IConnectionStringParse GetDefaultConnectionStringParse()
        {
            return null;
        }

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
}
