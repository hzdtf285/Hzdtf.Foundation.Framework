using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 持久化连接接口
    /// @ 黄振东
    /// </summary>
    public interface IPersistenceConnection
    {
        /// <summary>
        /// 主持久化连接信息
        /// </summary>
        PersistenceConectionInfo MasterPersistenceConnection { get; set; }

        /// <summary>
        /// 从持久化连接信息
        /// </summary>
        PersistenceConectionInfo SlavePersistenceConnection { get; set; }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        IDbConnection CreateDbConnection(string connectionString);

        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        string NewConnectionId(AccessMode accessMode= AccessMode.MASTER);

        /// <summary>
        /// 释放连接ID
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        void Release(string connectionId);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="isolation">事务级别</param>
        /// <returns>数据库事务</returns>
        IDbTransaction BeginTransaction(string connectionId, IsolationLevel isolation = IsolationLevel.ReadCommitted);

        /// <summary>
        /// 根据连接ID获取数据库事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库事务</returns>
        IDbTransaction GetDbTransaction(string connectionId, AccessMode accessMode = AccessMode.MASTER);
    }
}
