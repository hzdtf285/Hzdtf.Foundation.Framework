using Dapper;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.BasicFunction.Model.Standard;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 角色持久化
    /// @ 黄振东
    /// </summary>
    public partial class RolePersistence
    {
        /// <summary>
        /// 根据编码或名称查询角色列表
        /// </summary>
        /// <param name="role">编码</param>
        /// <param name="notId">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        public IList<RoleInfo> SelelctByCodeOrName(string code, string name, string connectionId = null)
        {
            IList<RoleInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE `{GetFieldByProp("Code")}`=@Code OR `{GetFieldByProp("Name")}`=@Name";
                result = dbConn.Query<RoleInfo>(sql, new { Code = code, Name = name }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据编码或名称查询角色列表，但排除ID
        /// </summary>
        /// <param name="role">编码</param>
        /// <param name="notId">名称</param>
        /// <param name="notId">排除ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        public IList<RoleInfo> SelelctByCodeOrNameNotId(string code, string name, int notId, string connectionId = null)
        {
            IList<RoleInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE Id!=@Id AND ([{GetFieldByProp("Code")}]=@Code OR [{GetFieldByProp("Name")}]=@Name)";
                result = dbConn.Query<RoleInfo>(sql, new { Id = notId, Code = code, Name = name }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询角色列表并去掉系统隐藏
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        public IList<RoleInfo> SelectAndNotSystemHide(string connectionId = null)
        {
            IList<RoleInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("SystemHide")}=@SystemHide";
                result = dbConn.Query<RoleInfo>(sql, new { SystemHide = false }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据筛选条件查询角色列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        public IList<RoleInfo> SelectByFilter(KeywordFilterInfo filter, string connectionId = null)
        {
            DynamicParameters parameters;
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);

            IList<RoleInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} " + whereSql.ToString();
                result = dbConn.Query<RoleInfo>(sql, parameters).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        #region 重写父类的方法

        /// <summary>
        /// 追加查询分页SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        protected override void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null)
        {
            whereSql.AppendFormat(" AND [{0}]=@SystemHide", GetFieldByProp("SystemHide"));
            parameters.Add("@SystemHide", false);

            AppendKeywordSql(whereSql, filter as KeywordFilterInfo);
        }

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected override string[] GetPageKeywordFields() => new string[] { GetFieldByProp("Code"), GetFieldByProp("Name") };

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "user_role", "role_id" },
                { "role_menu_function", "role_id" }
            };
        }

        #endregion
    }
}
