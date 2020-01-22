using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 角色持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IRolePersistence
    {
        /// <summary>
        /// 根据编码或名称查询角色列表
        /// </summary>
        /// <param name="role">编码</param>
        /// <param name="notId">名称</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        IList<RoleInfo> SelelctByCodeOrName(string code, string name, string connectionId = null);

        /// <summary>
        /// 根据编码或名称查询角色列表，但排除ID
        /// </summary>
        /// <param name="role">编码</param>
        /// <param name="notId">名称</param>
        /// <param name="notId">排除ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        IList<RoleInfo> SelelctByCodeOrNameNotId(string code, string name, int notId, string connectionId = null);

        /// <summary>
        /// 查询角色列表并去掉系统隐藏
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        IList<RoleInfo> SelectAndNotSystemHide(string connectionId = null);

        /// <summary>
        /// 根据筛选条件查询角色列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>角色列表</returns>
        IList<RoleInfo> SelectByFilter(KeywordFilterInfo filter, string connectionId = null);
    }
}
