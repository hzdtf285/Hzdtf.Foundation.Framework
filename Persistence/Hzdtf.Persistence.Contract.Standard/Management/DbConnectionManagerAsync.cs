using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Enums;
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
        /// <summary>
        /// 异步智能执行
        /// 根据连接ID会判断是否已经存在该连接，如果存在则直接引用存在的连接，否则新创建
        /// 因为执行业务方法是异步，本程序不自动关闭连接，由业务自行关闭
        /// </summary>
        /// <param name="connectionId">连接ID（如果需要执行一连串则设置相同连接ID，一旦传入该值，则不会关闭此链接，需要调用方关闭。[前提是先执行新建连接ID]]）</param>
        /// <param name="persistenceConnection">持久化连接</param>
        /// <param name="action">动作</param>
        /// <param name="accessMode">访问模式</param>
        public static void BrainpowerExecuteAsync(ref string connectionId, IPersistenceConnection persistenceConnection, Action<string, bool, IDbConnection> action, AccessMode accessMode = AccessMode.MASTER)
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

            action(connectionId, isClose, dbConnection);
        }
    }
}
