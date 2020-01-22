using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard
{
    /// <summary>
    /// 角色菜单功能服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IRoleMenuFunctionService
    {
        /// <summary>
        /// 根据角色ID查询菜单功能信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<MenuFunctionInfo>> QueryMenuFunctionsByRoleId(int roleId, string connectionId = null);

        /// <summary>
        /// 保存角色拥有的菜单功能信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuFunctionIds">菜单功能ID列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> SaveRoleMenuFunctions(int roleId, IList<int> menuFunctionIds, string connectionId = null);
    }
}
