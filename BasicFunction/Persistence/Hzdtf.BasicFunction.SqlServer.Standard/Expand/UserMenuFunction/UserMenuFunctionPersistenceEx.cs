using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 用户菜单持久化
    /// @ 黄振东
    /// </summary>
    public partial class UserMenuFunctionPersistence
    {
        /// <summary>
        /// 根据菜单编码、功能编码集合和用户ID统计个数
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        public int CountByMenuCodeAndFunctionCodesAndUserId(string menuCode, string[] functionCodes, int userId, string connectionId = null)
        {
            int result = 0;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MenuCode", menuCode);
            parameters.Add("@UserId", userId);

            StringBuilder functionSql = new StringBuilder();
            for (var i = 0; i < functionCodes.Length; i++)
            {
                string name = $"@FunctionCode{i}";
                functionSql.AppendFormat("{0},", name);
                parameters.Add(name, functionCodes[i]); ;
            }
            functionSql.Remove(functionSql.Length - 1, 1);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT COUNT(1) FROM menu M"
                        + " INNER JOIN menu_function MF ON M.[id]= MF.[menu_id]"
                        + $" INNER JOIN [function] F ON F.[id]= MF.[function_id] AND F.[code] IN({functionSql.ToString()})"
                        + $" INNER JOIN {Table} RMF ON RMF.[menu_function_id]=MF.[id] AND RMF.[user_id]=@UserId"
                        + " WHERE M.[code]=@MenuCode";
                result = dbConn.ExecuteScalar<int>(sql, parameters);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据菜单编码和用户ID查询功能信息列表
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>功能信息列表</returns>
        public IList<FunctionInfo> SelectFunctionsByMenuCodeAndUserId(string menuCode, int userId, string connectionId = null)
        {
            IList<FunctionInfo> result = null;

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT F.* FROM [function] F"
                        + " INNER JOIN menu_function MF ON MF.[function_id]= F.[id]"
                        + " INNER JOIN menu M ON m.[id]= MF.[menu_id] AND M.[code]=@MenuCode"
                        + $" INNER JOIN {Table} RMF ON RMF.[menu_function_id]=MF.[id] AND RMF.[user_id]=@UserId";
                result = dbConn.Query<FunctionInfo>(sql, new { MenuCode = menuCode, UserId = userId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }
    }
}
