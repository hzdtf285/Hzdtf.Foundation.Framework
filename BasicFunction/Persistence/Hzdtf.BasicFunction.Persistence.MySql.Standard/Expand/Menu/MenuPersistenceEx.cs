using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 菜单持久化
    /// </summary>
    public partial class MenuPersistence
    {
        /// <summary>
        /// 根据用户ID查询具有权限的用户菜单列表
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
                            + " INNER JOIN menu_function MF ON MF.`Id`= MF.`menu_id`"
                            + " INNER JOIN `function` F ON MF.`function_id`= F.`Id`"
                            + " INNER JOIN user_menu_function MFU ON MFU.`menu_function_id`= MF.`Id` AND MFU.`user_id`=@UserId";

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
    }
}
