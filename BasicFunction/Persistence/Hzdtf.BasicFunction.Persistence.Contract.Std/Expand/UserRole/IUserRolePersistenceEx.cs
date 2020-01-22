using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 用户角色持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserRolePersistence
    {
        /// <summary>
        /// 根据用户ID查询角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        IList<RoleInfo> SelectRolesByUserId(int userId, string connectionId = null);

        /// <summary>
        /// 根据用户ID集合查询用户角色列表，包含角色信息
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户角色列表</returns>
        IList<UserRoleInfo> SelectContainsRoleByUserIds(int[] userIds, string connectionId = null);

        /// <summary>
        /// 根据用户ID删除
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByUserId(int userId, string connectionId = null);

        /// <summary>
        /// 根据角色ID查询用户列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> SelectUsersByRoleId(int roleId, string connectionId = null);

        /// <summary>
        /// 根据角色编码查询用户列表
        /// </summary>
        /// <param name="roleCode">角色编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> SelectUsersByRoleCode(string roleCode, string connectionId = null);
    }
}
