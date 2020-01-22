using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 用户持久化
    /// @ 黄振东
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
        /// <param name="user">用户</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdatePasswordById(UserInfo user, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("Password")}`=@Password{GetModifyInfoSql(user)} WHERE {GetFieldByProp("Id") }=@Id";
                result = dbConn.Execute(sql, user, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据登录ID或编码或名称查询用户信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        public UserInfo SelectByLoginIdOrCodeOrName(string loginId, string code, string name, string connectionId = null)
        {
            UserInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE `{GetFieldByProp("LoginId")}`=@LoginId OR `{GetFieldByProp("Code")}`=@Code OR `{GetFieldByProp("Name")}`=@Name";
                result = dbConn.QueryFirstOrDefault<UserInfo>(sql, new { LoginId = loginId, Code = code, Name = name });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据登录ID或编码或名称查询用户信息，但排除ID
        /// </summary>
        /// <param name="notId">排除ID</param>
        /// <param name="loginId">登录ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        public UserInfo SelectByLoginIdOrCodeOrNameNotId(int notId, string loginId, string code, string name, string connectionId = null)
        {
            UserInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE `{GetFieldByProp("Id")}`!=@Id AND (`{GetFieldByProp("LoginId")}`=@LoginId OR `{GetFieldByProp("Code")}`=@Code OR `{GetFieldByProp("Name")}`=@Name)";
                result = dbConn.QueryFirstOrDefault<UserInfo>(sql, new { Id = notId, LoginId = loginId, Code = code, Name = name });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据筛选条件查询用户列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        public IList<UserInfo> SelectByFilter(UserFilterInfo filter, string connectionId = null)
        {
            DynamicParameters parameters;
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);

            IList<UserInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} " + whereSql.ToString();
                result = dbConn.Query<UserInfo>(sql, parameters).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        #region 重写父类的方法

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected override string[] GetPageKeywordFields() => new string[]
        {
            GetFieldByProp("Code"),
            GetFieldByProp("Name"),
            GetFieldByProp("LoginId")
        };

        /// <summary>
        /// 追加查询分页SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        protected override void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null)
        {
            whereSql.AppendFormat(" AND `{0}`=@SystemHide", GetFieldByProp("SystemHide"));
            parameters.Add("@SystemHide", false);

            if (filter is UserFilterInfo)
            {
                UserFilterInfo userFilter = filter as UserFilterInfo;
                if (userFilter.Enabled != null)
                {
                    whereSql.AppendFormat(" AND `{0}`=@Enabled", GetFieldByProp("Enabled"));
                    parameters.Add("@Enabled", userFilter.Enabled);
                }
            }
        }

        /// <summary>
        /// 获取查询分页连接SQL
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <returns>连接SQL语句</returns>
        protected override string GetSelectPageJoinSql(DynamicParameters parameters, FilterInfo filter = null)
        {
            if (filter is UserFilterInfo)
            {
                UserFilterInfo userFilter = filter as UserFilterInfo;
                if (userFilter.RoleId == null)
                {
                    return null;
                }

                parameters.Add("@RoleId", userFilter.RoleId);
                return "INNER JOIN user_role UR ON UR.user_id=`user`.id AND UR.role_id=@RoleId";
            }

            return null;
        }

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "user_role", "user_id" },
                { "user_menu_function", "user_id" }
            };
        }

        #endregion
    }
}

