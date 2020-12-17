using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using System;
using System.Data;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 持久化连接基类
    /// @ 黄振东
    /// </summary>
    public abstract class PersistenceConnectionBase : IPersistenceConnection
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public IDefaultConnectionString DefaultConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 主持久化连接信息
        /// </summary>
        private PersistenceConectionInfo masterPersistenceConnection;

        /// <summary>
        /// 主持久化连接信息
        /// </summary>
        public PersistenceConectionInfo MasterPersistenceConnection
        {
            get
            {
                return CreatePersistenceConnection(masterPersistenceConnection, GetMasterConnectionString(), AccessMode.MASTER, p =>
                {
                    masterPersistenceConnection = p;
                });
            }
            set { masterPersistenceConnection = value; }
        }

        /// <summary>
        /// 从持久化连接信息
        /// </summary>
        private PersistenceConectionInfo slavePersistenceConnection;

        /// <summary>
        /// 从持久化连接信息
        /// </summary>
        public PersistenceConectionInfo SlavePersistenceConnection
        {
            get
            {
                return CreatePersistenceConnection(slavePersistenceConnection, GetSlaveConnectionString(), AccessMode.SLAVE, p =>
                {
                    slavePersistenceConnection = p;
                });
            }
            set { slavePersistenceConnection = value; }
        }

        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        public string NewConnectionId(AccessMode accessMode = AccessMode.MASTER) => DbConnectionManager.NewConnectionId(this, accessMode);

        /// <summary>
        /// 释放连接ID
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        public void Release(string connectionId) => DbConnectionManager.Release(connectionId);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="isolation">事务级别</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction BeginTransaction(string connectionId, IsolationLevel isolation = IsolationLevel.ReadCommitted) => DbConnectionManager.BeginTransaction(connectionId, this, isolation);

        /// <summary>
        /// 根据连接ID获取数据库事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction GetDbTransaction(string connectionId, AccessMode accessMode = AccessMode.MASTER) => DbConnectionManager.GetDbTransaction(connectionId, this, accessMode);

        /// <summary>
        /// 获取主连接字符串
        /// </summary>
        /// <returns>主连接字符串</returns>
        protected virtual string GetMasterConnectionString() => DefaultConnectionString.Connections[0];

        /// <summary>
        /// 获取从连接字符串
        /// </summary>
        /// <returns>从连接字符串</returns>
        protected virtual string GetSlaveConnectionString() => DefaultConnectionString.Connections[1];

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        public abstract IDbConnection CreateDbConnection(string connectionString);

        /// <summary>
        /// 创建持久化连接信息
        /// </summary>
        /// <param name="persistenceConection">持久化连接</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="accessMode">访问模式</param>
        /// <param name="setAction">设置动作</param>
        /// <returns>持久化连接信息</returns>
        private PersistenceConectionInfo CreatePersistenceConnection(PersistenceConectionInfo persistenceConection, string connectionString, AccessMode accessMode, Action<PersistenceConectionInfo> setAction)
        { 
            if (persistenceConection != null || string.IsNullOrWhiteSpace(connectionString))
            {
                return persistenceConection;
            }

            persistenceConection = new PersistenceConectionInfo()
            {
                ConnectionString = connectionString,
                AccessMode = accessMode
            };

            setAction(persistenceConection);

            return persistenceConection;
        }
    }
}
