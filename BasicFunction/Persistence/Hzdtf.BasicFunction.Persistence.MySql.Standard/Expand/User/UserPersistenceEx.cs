using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 用户持久化
    /// </summary>
    public partial class UserPersistence
    {
        /// <summary>
        /// 根据登录ID和密码查询用户信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="password">密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        public UserInfo SelectByLoginIdAndPassword(string loginId, string password, string connectionId = null)
        {
            UserInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("LoginId")}=@LoginId AND `{GetFieldByProp("Password")}`=@Password";
                result = dbConn.QueryFirstOrDefault<UserInfo>(sql, new { LoginId = loginId, Password = password });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID更新密码
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="password">密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdatePasswordById(int id, string password, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("Password")}`=@Password WHERE {GetFieldByProp("Id") }= @Id";
                result = dbConn.Execute(sql, new { Id = id, password = password });
            });

            return result;
        }
    }
}

