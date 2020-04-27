using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Management
{
    /// <summary>
    /// 数据库连接管理器
    /// @ 黄振东
    /// </summary>
    public static partial class DbConnectionManager
    {
        #region 属性与字段

        /// <summary>
        /// 数据库连接字典
        /// </summary>
        private static readonly IDictionary<string, IList<DbConnectionEx>> dicDbConnections = new Dictionary<string, IList<DbConnectionEx>>();

        /// <summary>
        /// 当前数据库连接数
        /// </summary>
        public static int CurrDbConnCount
        {
            get => dicDbConnections.Count;
        }

        /// <summary>
        /// 数据库事务字典
        /// </summary>
        private static readonly IDictionary<string, DbTransactionEx> dicDbTransaction = new Dictionary<string, DbTransactionEx>();

        /// <summary>
        /// 同步数据库连接字典
        /// </summary>
        private static readonly object syncDicDbConnection = new object();

        /// <summary>
        /// 同步数据库事务字典
        /// </summary>
        private static readonly object syncDicDbTransaction = new object();

        #endregion

        #region 执行数据库方法

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection GetDbConnection(string connectionId, IPersistenceConnection persistenceConnection, out string connectionString, AccessMode accessMode = AccessMode.MASTER)
        {
            bool isExistsConnection;
            return GetDbConnection(connectionId, persistenceConnection, out isExistsConnection, out connectionString, accessMode);
        }

        /// <summary>
        /// 智能执行
        /// 根据连接ID会判断是否已经存在该连接，如果存在则直接引用存在的连接，否则新创建
        /// </summary>
        /// <param name="connectionId">连接ID（如果需要执行一连串则设置相同连接ID，一旦传入该值，则不会关闭此链接，需要调用方关闭。[前提是先执行新建连接ID]]）</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="action">动作</param>
        /// <param name="accessMode">访问模式</param>
        public static void BrainpowerExecute(string connectionId, IPersistenceConnection persistenceConnection, Action<string, IDbConnection> action, AccessMode accessMode = AccessMode.MASTER)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                connectionId = NewConnectionId(persistenceConnection, accessMode);
                isClose = true;
            }

            bool isExistsConnection;
            string connectionString;
            IDbConnection dbConnection = GetDbConnection(connectionId, persistenceConnection, out isExistsConnection, out connectionString, accessMode);
            
            // 如果不是新建连接ID且不存在连接，则需要本次连接关闭
            if (!isExistsConnection && !isClose)
            {
                isClose = true;
            }

            try
            {
                action(connectionId, dbConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Release(connectionId, dbConnection);
                }
            }
        }

        #endregion

        #region 事务相关

        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="persistenceConnection">持久化连接</param>>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        public static string NewConnectionId(IPersistenceConnection persistenceConnection, AccessMode accessMode = AccessMode.MASTER)
        {
            PersistenceConectionInfo perConnInfo = GetPersistenceConnectionInfo(persistenceConnection, accessMode);
            string connId = StringUtil.NewShortGuid();
            AddDicValue(connId, new DbConnectionEx()
            {
                ConnectionString = perConnInfo.ConnectionString
            });

            return connId;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="isolation">事务级别</param>
        /// <returns>数据库事务</returns>
        public static IDbTransaction BeginTransaction(string connectionId, IPersistenceConnection persistenceConnection, IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                throw new ArgumentNullException("连接ID不能为null或空");
            }

            string connectionString;
            IDbConnection dbConnection = GetDbConnection(connectionId, persistenceConnection, out connectionString);
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            IDbTransaction dbTransaction = dbConnection.BeginTransaction(isolation);
            if (dicDbTransaction.ContainsKey(connectionId))
            {
                lock (syncDicDbTransaction)
                {
                    dicDbTransaction[connectionId] = new DbTransactionEx()
                    {
                        DbTransaction = dbTransaction,
                        ConnectionString = connectionString
                    };
                }
            }
            else
            {
                lock (syncDicDbTransaction)
                {
                    try
                    {
                        dicDbTransaction.Add(connectionId, new DbTransactionEx()
                        {
                            DbTransaction = dbTransaction,
                            ConnectionString = connectionString
                        });
                    }
                    catch (ArgumentException) { }
                }
            }

            return dbTransaction;
        }

        #endregion

        #region 释放资源

        /// <summary>
        /// 释放所有
        /// </summary>
        public static void ReleaseAll()
        {
            if (!dicDbConnections.IsNullOrCount0())
            {
                foreach (KeyValuePair<string, IList<DbConnectionEx>> keyValue in dicDbConnections)
                {
                    if (keyValue.Value.IsNullOrCount0())
                    {
                        continue;
                    }

                    foreach (DbConnectionEx dbConnection in keyValue.Value)
                    {
                        if (dbConnection != null)
                        {
                            try
                            {
                                dbConnection.DbConnection.Close();
                                dbConnection.DbConnection.Dispose();
                            }
                            catch { }
                        }
                    }
                }

                dicDbConnections.Clear();
            }

            dicDbTransaction.Clear();
        }

        /// <summary>
        /// 根据连接ID和数据库连接释放
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="dbConnection">数据库连接</param>
        public static void Release(string connectionId, IDbConnection dbConnection)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                return;
            }

            if (dicDbConnections.ContainsKey(connectionId))
            {
                IList<DbConnectionEx> dbConnectionExs = dicDbConnections[connectionId];
                if (dbConnectionExs.IsNullOrCount0())
                {
                    dicDbConnections.Remove(connectionId);
                    return;
                }

                int needRemoveIndex = -1;
                for (var i = 0; i < dbConnectionExs.Count; i++)
                {
                    var dbConnectionEx = dbConnectionExs[i];
                    if (dbConnectionEx.DbConnection == dbConnection)
                    {
                        if (dbConnectionEx != null)
                        {
                            try
                            {
                                dbConnectionEx.DbConnection.Close();
                                dbConnectionEx.DbConnection.Dispose();
                            }
                            catch { }
                        }

                        needRemoveIndex = i;

                        break;
                    }
                }

                if (needRemoveIndex != -1)
                {
                    dbConnectionExs.RemoveAt(needRemoveIndex);
                    if (dbConnectionExs.IsNullOrCount0())
                    {
                        dicDbConnections.Remove(connectionId);
                    }
                }
            }

            if (dicDbTransaction.ContainsKey(connectionId))
            {
                lock (syncDicDbTransaction)
                {
                    dicDbTransaction.Remove(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据连接ID释放
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        public static void Release(string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                return;
            }

            if (dicDbConnections.ContainsKey(connectionId))
            {
                IList<DbConnectionEx> dbConnections = dicDbConnections[connectionId];
                if (dbConnections.IsNullOrCount0())
                {
                    lock (syncDicDbConnection)
                    {
                        dicDbConnections.Remove(connectionId);
                    }
                }
                else
                {
                    foreach (DbConnectionEx dbConnection in dbConnections)
                    {
                        if (dbConnection != null)
                        {
                            try
                            {
                                dbConnection.DbConnection.Close();
                                dbConnection.DbConnection.Dispose();
                            }
                            catch { }
                        }
                    }
                }

                lock (syncDicDbConnection)
                {
                    dicDbConnections.Remove(connectionId);
                }
            }
           
            if (dicDbTransaction.ContainsKey(connectionId))
            {
                lock (syncDicDbTransaction)
                {
                    dicDbTransaction.Remove(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据连接ID获取数据库事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库事务</returns>
        public static IDbTransaction GetDbTransaction(string connectionId, IPersistenceConnection persistenceConnection, AccessMode accessMode = AccessMode.MASTER)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                return null;
            }

            if (dicDbTransaction.ContainsKey(connectionId))
            {
                DbTransactionEx dbTransactionEx = dicDbTransaction[connectionId];
                if (dbTransactionEx == null)
                {
                    return null;
                }

                PersistenceConectionInfo persistenceConectionInfo = GetPersistenceConnectionInfo(persistenceConnection, accessMode);
                if (dbTransactionEx.ConnectionString.Equals(persistenceConectionInfo.ConnectionString))
                {
                    return dbTransactionEx.DbTransaction;
                }
            }

            return null;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取持久化连接信息
        /// </summary>
        /// <param name="persistenceConnection">持久化连接</param>>
        /// <param name="accessMode">访问模式</param>
        /// <returns>持久化连接信息</returns>
        private static PersistenceConectionInfo GetPersistenceConnectionInfo(IPersistenceConnection persistenceConnection, AccessMode accessMode = AccessMode.MASTER)
        {
            PersistenceConectionInfo perConnInfo = null;
            if (accessMode == AccessMode.MASTER)
            {
                perConnInfo = persistenceConnection.MasterPersistenceConnection;
            }
            else
            {
                perConnInfo = persistenceConnection.SlavePersistenceConnection;
                // 如果未找到从，则找主
                if (perConnInfo == null || string.IsNullOrWhiteSpace(perConnInfo.ConnectionString))
                {
                    perConnInfo = new PersistenceConectionInfo()
                    {
                        AccessMode = AccessMode.SLAVE,
                        ConnectionString = persistenceConnection.MasterPersistenceConnection.ConnectionString
                    };
                }
            }

            if (perConnInfo == null || string.IsNullOrWhiteSpace(perConnInfo.ConnectionString))
            {
                throw new Exception("未找到连接字符串");
            }

            return perConnInfo;
        }

        #endregion

        #region 键值缓存

        /// <summary>
        /// 添加键值
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="dbConnectionEx">数据库连接</param>
        private static void AddDicValue(string connectionId, DbConnectionEx dbConnectionEx)
        {
            if (dicDbConnections.ContainsKey(connectionId))
            {
                if (dbConnectionEx == null)
                {
                    return;
                }

                foreach (DbConnectionEx db in dicDbConnections[connectionId])
                {
                    if (db.ConnectionString.Equals(dbConnectionEx.ConnectionString))
                    {
                        if (dbConnectionEx.DbConnection != null)
                        {
                            db.DbConnection = dbConnectionEx.DbConnection;
                        }
                        return;
                    }
                }

                dicDbConnections[connectionId].Add(dbConnectionEx);
            }
            else
            {
                IList<DbConnectionEx> dbConnections = new List<DbConnectionEx>();
                if (dbConnectionEx != null)
                {
                    dbConnections.Add(dbConnectionEx);
                }
                lock (syncDicDbConnection)
                {
                    try
                    {
                        dicDbConnections.Add(connectionId, dbConnections);
                    }
                    catch (ArgumentException) { }
                }
            }
        }

        /// <summary>
        /// 从缓存里获取数据库连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="isExistsConnection">是否存在连接</param>
        /// <returns>数据库连接</returns>
        private static IDbConnection GetDbConnectionFromCache(string connectionId, string connectionString, out bool isExistsConnection)
        {
            isExistsConnection = false;
            if (dicDbConnections.ContainsKey(connectionId))
            {
                IList<DbConnectionEx> dbs = dicDbConnections[connectionId];
                foreach (DbConnectionEx db in dbs)
                {
                    if (db.ConnectionString.Equals(connectionString))
                    {
                        isExistsConnection = true;
                        return db.DbConnection;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="isExistsConnection">是否存在连接</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库连接</returns>
        private static IDbConnection GetDbConnection(string connectionId, IPersistenceConnection persistenceConnection, out bool isExistsConnection,
            out string connectionString, AccessMode accessMode = AccessMode.MASTER)
        {
            isExistsConnection = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                throw new ArgumentNullException("连接ID不能为null或空");
            }

            PersistenceConectionInfo perConnInfo = GetPersistenceConnectionInfo(persistenceConnection, accessMode);
            connectionString = perConnInfo.ConnectionString;

            IDbConnection dbConnection = GetDbConnectionFromCache(connectionId, perConnInfo.ConnectionString, out isExistsConnection);
            if (dbConnection == null)
            {
                dbConnection = persistenceConnection.CreateDbConnection(perConnInfo.ConnectionString);
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                AddDicValue(connectionId, new DbConnectionEx()
                {
                    ConnectionString = perConnInfo.ConnectionString,
                    DbConnection = dbConnection
                });
            }

            return dbConnection;
        }

        #endregion
    }

    /// <summary>
    /// 数据库连接扩展
    /// </summary>
    sealed class DbConnectionEx
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection DbConnection
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 数据库事务扩展
    /// </summary>
    sealed class DbTransactionEx
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库事务
        /// </summary>
        public IDbTransaction DbTransaction
        {
            get;
            set;
        }
    }
}
