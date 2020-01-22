using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 用户角色持久化
    /// @ 黄振东
    /// </summary>
    public partial class UserRolePersistence
    {
        /// <summary>
        /// 角色持久化
        /// </summary>
        public RolePersistence RolePersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 用户持久化
        /// </summary>
        public UserPersistence UserPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 根据用户ID查询角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        public IList<RoleInfo> SelectRolesByUserId(int userId, string connectionId = null)
        {
            IList<RoleInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"SELECT {RolePersistence.AllFieldMapProps().JoinSelectPropMapFields("R.")} FROM [{RolePersistence.Table}] R"
                    + $" INNER JOIN [{Table}] UR ON R.id=UR.role_id AND UR.user_id=@UserId";
                result = dbConn.Query<RoleInfo>(sql, new { UserId = userId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据用户ID删除
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int DeleteByUserId(int userId, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{DeleteSql()} WHERE {GetFieldByProp("UserId")}=@UserId";
                result = dbConn.Execute(sql, new { UserId = userId }, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据用户ID集合查询用户角色列表，包含角色信息
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户角色列表</returns>
        public IList<UserRoleInfo> SelectContainsRoleByUserIds(int[] userIds, string connectionId = null)
        {
            IList<UserRoleInfo> result = null;
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder userIdSql = new StringBuilder();
            for (int i = 0; i < userIds.Length; i++)
            {
                string name = $"@UserId{i}";
                parameters.Add(name, userIds[i]);
                userIdSql.AppendFormat("{0},", name);
            }
            userIdSql.Remove(userIdSql.Length - 1, 1);
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"SELECT UR.user_id UserId,{RolePersistence.AllFieldMapProps().JoinSelectPropMapFields("R.")} FROM {Table} UR"
                    + $" INNER JOIN [{RolePersistence.Table}] R ON R.id=UR.role_id AND UR.user_id IN({userIdSql.ToString()})";
                result = dbConn.Query<UserRoleInfo, RoleInfo, UserRoleInfo>(sql, (ur, r) =>
                {
                    ur.Role = r;
                    return ur;
                }, parameters).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据角色ID查询用户列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        public IList<UserInfo> SelectUsersByRoleId(int roleId, string connectionId = null)
        {
            IList<UserInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"SELECT {UserPersistence.AllFieldMapProps().JoinSelectPropMapFields("U.")} FROM [{UserPersistence.Table}] U"
                    + $" INNER JOIN [{Table}] UR ON U.id=UR.user_id AND UR.role_id=@RoleId AND U.system_hide=@SystemHide";
                result = dbConn.Query<UserInfo>(sql, new { RoleId = roleId, SystemHide = false }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据角色编码查询用户列表
        /// </summary>
        /// <param name="roleCode">角色编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        public IList<UserInfo> SelectUsersByRoleCode(string roleCode, string connectionId = null)
        {
            IList<UserInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"SELECT {UserPersistence.AllFieldMapProps().JoinSelectPropMapFields("U.")} FROM [{UserPersistence.Table}] U"
                    + $" INNER JOIN [{Table}] UR ON U.id=UR.user_id AND U.system_hide=@SystemHide AND U.enabled=@Enabled"
                    + $" INNER JOIN [{RolePersistence.Table}] R ON R.id=UR.role_id AND R.code=@RoleCode";
                result = dbConn.Query<UserInfo>(sql, new { RoleCode = roleCode, SystemHide = false, Enabled = true }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }
    }
}
