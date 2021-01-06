using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard.User
{
    /// <summary>
    /// 用户菜单服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserMenuService
    {
        /// <summary>
        /// 根据用户ID获取能访问的菜单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<UserMenuInfo> CanAccessMenus(int userId, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
