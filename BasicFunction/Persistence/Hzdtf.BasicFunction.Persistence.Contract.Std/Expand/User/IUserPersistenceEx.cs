using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 用户持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserPersistence
    {
        /// <summary>
        /// 根据登录ID和密码查询用户信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="password">密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        UserInfo SelectByLoginIdAndPassword(string loginId, string password, string connectionId = null);

        /// <summary>
        /// 根据ID更新密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdatePasswordById(UserInfo user, string connectionId = null);

        /// <summary>
        /// 根据登录ID或编码或名称查询用户信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        UserInfo SelectByLoginIdOrCodeOrName(string loginId, string code, string name, string connectionId = null);

        /// <summary>
        /// 根据登录ID或编码或名称查询用户信息，但排除ID
        /// </summary>
        /// <param name="notId">排除ID</param>
        /// <param name="loginId">登录ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户信息</returns>
        UserInfo SelectByLoginIdOrCodeOrNameNotId(int notId, string loginId, string code, string name, string connectionId = null);

        /// <summary>
        /// 根据筛选条件查询用户列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> SelectByFilter(UserFilterInfo filter, string connectionId = null);
    }
}
