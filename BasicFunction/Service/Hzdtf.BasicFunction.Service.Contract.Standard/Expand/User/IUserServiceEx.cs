using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard
{
    /// <summary>
    /// 用户服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserService
    {
        /// <summary>
        /// 根据登录ID修改密码
        /// </summary>
        /// <param name="currUserModifyPassword">当前用户修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyPasswordByLoginId(CurrUserModifyPasswordInfo currUserModifyPassword, string connectionId = null);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="modifyPassword">修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ResetUserPassword(ModifyPasswordInfo modifyPassword, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和功能编码判断当前用户是否有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCode">功能编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserPermission(string menuCode, string functionCode, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和功能编码集合判断当前用户是否有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserPermission(string menuCode, string[] functionCodes, string connectionId = null);

        /// <summary>
        /// 根据用户ID、菜单编码和功能编码集合判断用户是否有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsPermission(int userId, string menuCode, string[] functionCodes, string connectionId = null);

        /// <summary>
        /// 根据菜单编码查询当前用户所拥有的功能信息列表
        /// </summary>
        /// <param name="roleIds">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<FunctionInfo>> QueryCurrUserOwnFunctionsByMenuCode(string menuCode, string connectionId = null);

        /// <summary>
        /// 根据用户ID和菜单编码查询用户所拥有的功能信息列表
        /// </summary>
        /// <param name="roleIds">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<FunctionInfo>> QueryUserOwnFunctionsByMenuCode(int userId, string menuCode, string connectionId = null);

        /// <summary>
        /// 判断当前用户是否是系统管理组
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserAdministrators(string connectionId = null);

        /// <summary>
        /// 判断用户是否是系统管理组
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsUserAdministrators(int userId, string connectionId = null);

        /// <summary>
        /// 根据筛选条件查询用户列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<UserInfo>> QueryByFilter(UserFilterInfo filter, string connectionId = null);
    }
}
