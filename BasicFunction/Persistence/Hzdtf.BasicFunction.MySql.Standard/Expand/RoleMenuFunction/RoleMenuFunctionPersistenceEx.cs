using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 角色菜单持久化
    /// @ 黄振东
    /// </summary>
    public partial class RoleMenuFunctionPersistence
    {
        /// <summary>
        /// 功能持久化
        /// </summary>
        public FunctionPersistence FunctionPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单功能持久化
        /// </summary>
        public MenuFunctionPersistence MenuFunctionPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 根据菜单编码、功能编码集合和角色ID集合统计个数
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        public int CountByMenuCodeAndFunctionCodesAndRoleIds(string menuCode, string[] functionCodes, int[] roleIds, string connectionId = null)
        {
            int result = 0;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MenuCode", menuCode);

            StringBuilder functionSql = new StringBuilder();
            for (var i = 0; i < functionCodes.Length; i++)
            {
                string name = $"@FunctionCode{i}";
                functionSql.AppendFormat("{0},", name);
                parameters.Add(name, functionCodes[i]); ;
            }
            functionSql.Remove(functionSql.Length - 1, 1);

            StringBuilder roleSql = new StringBuilder();
            for (var i = 0; i < roleIds.Length; i++)
            {
                string name = $"@RoleId{i}";
                roleSql.AppendFormat("{0},", name);
                parameters.Add(name, roleIds[i]);;
            }
            roleSql.Remove(roleSql.Length - 1, 1);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT COUNT(*) FROM menu M"
                        + " INNER JOIN menu_function MF ON M.`id`= MF.`menu_id`"
                        + $" INNER JOIN `function` F ON F.`id`= MF.`function_id` AND F.`code` IN({functionSql.ToString()})"
                        + $" INNER JOIN {Table} RMF ON RMF.`menu_function_id`=MF.`id` AND RMF.`role_id` IN({roleSql.ToString()})"
                        + " WHERE M.`code`=@MenuCode";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "CountByMenuCodeAndFunctionCodesAndRoleIds");
                result = dbConn.ExecuteScalar<int>(sql, parameters);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据菜单编码和角色ID集合查询功能信息列表
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>功能信息列表</returns>
        public IList<FunctionInfo> SelectFunctionsByMenuCodeAndRoleIds(string menuCode, int[] roleIds, string connectionId = null)
        {
            IList<FunctionInfo> result = null;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MenuCode", menuCode);

            StringBuilder roleSql = new StringBuilder();
            for (var i = 0; i < roleIds.Length; i++)
            {
                string name = $"@RoleId{i}";
                roleSql.AppendFormat("{0},", name);
                parameters.Add(name, roleIds[i]); ;
            }
            roleSql.Remove(roleSql.Length - 1, 1);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT F.* FROM `function` F"
                        + " INNER JOIN menu_function MF ON MF.`function_id`= F.`id`"
                        + " INNER JOIN menu M ON M.`id`= MF.`menu_id` AND M.`code`=@MenuCode"
                        + $" INNER JOIN {Table} RMF ON RMF.`menu_function_id`= MF.`id` AND RMF.`role_id` IN({roleSql.ToString()})";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectFunctionsByMenuCodeAndRoleIds");
                result = dbConn.Query<FunctionInfo>(sql, parameters).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据角色ID查询菜单功能信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单功能信息列表</returns>
        public IList<MenuFunctionInfo> SelectMenuFunctionsByRoleId(int roleId, string connectionId = null)
        {
            IList<MenuFunctionInfo> result = null;

            string fields = MenuFunctionPersistence.AllFieldMapProps().JoinSelectPropMapFields(pfx: "MF.");

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"select {fields} from {MenuFunctionPersistence.Table} MF"
                            + $" inner join {Table} RMF on RMF.menu_function_id = MF.id AND RMF.role_id=@RoleId";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectMenuFunctionsByRoleId");
                result = dbConn.Query<MenuFunctionInfo>(sql, new { RoleId = roleId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据用户ID删除
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int DeleteByRoleId(int roleId, string connectionId = null)
        {
            int result = 0;

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                var sql = BasicDeleteSql();
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteByRoleId");
                result = dbConn.Execute($"{sql} WHERE role_id=@RoleId", new { RoleId = roleId }, GetDbTransaction(connId));
            });

            return result;
        }
    }
}
