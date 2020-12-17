using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 菜单持久化
    /// @ 黄振东
    /// </summary>
    public partial class MenuPersistence
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
        /// 根据用户ID查询具有权限的菜单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        public IList<MenuInfo> SelectByUserId(int userId, string connectionId = null)
        {
            string menuFields = JoinSelectPropMapFields(pfx: "M.");

            List<MenuInfo> result = new List<MenuInfo>();
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT " + menuFields + ", F.id, F.`code`, F.`name` FROM menu M"
                            + " INNER JOIN menu_function MF ON M.`Id`=MF.`menu_id`"
                            + " INNER JOIN `function` F ON MF.`function_id`=F.`Id`"
                            + " INNER JOIN user_menu_function MFU ON MFU.`menu_function_id`=MF.`Id` AND MFU.`user_id`=@UserId";

                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByUserId");
                dbConn.Query<MenuInfo, FunctionInfo, MenuInfo>(sql, (x, y) =>
                {
                    MenuInfo existsMenu = result.Find(q => q.Id == x.Id);
                    if (existsMenu == null)
                    {
                        result.Add(x);

                        existsMenu = x;
                        existsMenu.Functions = new List<FunctionInfo>();
                        existsMenu.Functions.Add(y);

                        return existsMenu;
                    }

                    List<FunctionInfo> existsFuns = existsMenu.Functions as List<FunctionInfo>;
                    if (existsFuns.Exists(q => q.Id == y.Id))
                    {
                        return null;
                    }

                    existsMenu.Functions.Add(y);

                    return null;

                }, new { UserId = userId }, splitOn: "Id").AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据角色ID集合查询具有权限的菜单列表
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        public IList<MenuInfo> SelectByRoleIds(int[] roleIds, string connectionId = null)
        {
            string menuFields = JoinSelectPropMapFields(pfx: "M.");
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder roleSql = new StringBuilder();
            for (var i = 0; i < roleIds.Length; i++)
            {
                string name = $"@RoleId{i}";
                roleSql.AppendFormat("{0},", name);
                parameters.Add(name, roleIds[i]); ;
            }
            roleSql.Remove(roleSql.Length - 1, 1);

            List<MenuInfo> result = new List<MenuInfo>();
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT " + menuFields + ", F.id, F.`code`, F.`name` FROM menu M"
                            + " INNER JOIN menu_function MF ON M.`Id`=MF.`menu_id`"
                            + " INNER JOIN `function` F ON MF.`function_id`=F.`Id`"
                            + $" INNER JOIN role_menu_function MFU ON MFU.`menu_function_id`= MF.`Id` AND MFU.`role_id` IN({roleSql.ToString()})";

                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByRoleIds");
                dbConn.Query<MenuInfo, FunctionInfo, MenuInfo>(sql, (x, y) =>
                {
                    MenuInfo existsMenu = result.Find(q => q.Id == x.Id);
                    if (existsMenu == null)
                    {
                        result.Add(x);

                        existsMenu = x;
                        existsMenu.Functions = new List<FunctionInfo>();
                        existsMenu.Functions.Add(y);

                        return existsMenu;
                    }

                    List<FunctionInfo> existsFuns = existsMenu.Functions as List<FunctionInfo>;
                    if (existsFuns.Exists(q => q.Id == y.Id))
                    {
                        return null;
                    }

                    existsMenu.Functions.Add(y);

                    return null;

                }, parameters, splitOn: "Id").AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询所有菜单列表，包含所属的功能列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        public IList<MenuInfo> SelectContainsFunctions(string connectionId = null)
        {
            string menuFields = JoinSelectPropMapFields(pfx: "M.");
            string funFields = FunctionPersistence.AllFieldMapProps().JoinSelectPropMapFields("F.", true);

            List<MenuInfo> result = new List<MenuInfo>();
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = "SELECT " + menuFields + ",F.id Id," + funFields + ",MF.id MenuFunctionId FROM menu M"
                            + " INNER JOIN menu_function MF ON M.`Id`=MF.`menu_id`"
                            + " INNER JOIN `function` F ON MF.`function_id`= F.`Id`";

                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectContainsFunctions");
                dbConn.Query<MenuInfo, FunctionInfo, MenuInfo>(sql, (x, y) =>
                {
                    MenuInfo existsMenu = result.Find(q => q.Id == x.Id);
                    if (existsMenu == null)
                    {
                        result.Add(x);

                        existsMenu = x;
                        existsMenu.Functions = new List<FunctionInfo>();
                        existsMenu.Functions.Add(y);

                        return existsMenu;
                    }

                    List<FunctionInfo> existsFuns = existsMenu.Functions as List<FunctionInfo>;
                    if (existsFuns.Exists(q => q.Id == y.Id))
                    {
                        return null;
                    }

                    existsMenu.Functions.Add(y);

                    return null;

                }, splitOn: "Id").AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        #region 重写父类的方法

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "menu_function", "menu_id" }
            };
        }

        #endregion
    }
}
