using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 菜单持久化
    /// @ 黄振东
    /// </summary>
    public partial interface IMenuPersistence
    {
        /// <summary>
        /// 根据用户ID查询具有权限的菜单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        IList<MenuInfo> SelectByUserId(int userId, string connectionId = null);

        /// <summary>
        /// 根据角色ID集合查询具有权限的菜单列表
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        IList<MenuInfo> SelectByRoleIds(int[] roleIds, string connectionId = null);

        /// <summary>
        /// 查询所有菜单列表，包含所属的功能列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单列表</returns>
        IList<MenuInfo> SelectContainsFunctions(string connectionId = null);
    }
}
