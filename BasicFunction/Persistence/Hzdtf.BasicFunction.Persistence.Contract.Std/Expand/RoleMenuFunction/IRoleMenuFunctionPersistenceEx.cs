using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 角色菜单持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IRoleMenuFunctionPersistence
    {
        /// <summary>
        /// 根据菜单编码、功能编码集合和角色ID集合统计个数
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        int CountByMenuCodeAndFunctionCodesAndRoleIds(string menuCode, string[] functionCodes, int[] roleIds, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和角色ID集合查询功能信息列表
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>功能信息列表</returns>
        IList<FunctionInfo> SelectFunctionsByMenuCodeAndRoleIds(string menuCode, int[] roleIds, string connectionId = null);

        /// <summary>
        /// 根据角色ID查询菜单功能信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>菜单功能信息列表</returns>
        IList<MenuFunctionInfo> SelectMenuFunctionsByRoleId(int roleId, string connectionId = null);

        /// <summary>
        /// 根据用户ID删除
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByRoleId(int roleId, string connectionId = null);
    }
}
