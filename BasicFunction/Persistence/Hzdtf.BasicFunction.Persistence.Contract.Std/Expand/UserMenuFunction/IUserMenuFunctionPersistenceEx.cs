using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 用户菜单持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserMenuFunctionPersistence
    {
        /// <summary>
        /// 根据菜单编码、功能编码集合和用户ID统计个数
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        int CountByMenuCodeAndFunctionCodesAndUserId(string menuCode, string[] functionCodes, int userId, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和用户ID查询功能信息列表
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>功能信息列表</returns>
        IList<FunctionInfo> SelectFunctionsByMenuCodeAndUserId(string menuCode, int userId, string connectionId = null);
    }
}
