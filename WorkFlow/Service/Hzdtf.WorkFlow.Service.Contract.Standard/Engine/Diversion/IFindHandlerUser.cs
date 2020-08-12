using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Diversion
{
    /// <summary>
    /// 查找处理者用户接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFindHandlerUser
    {
        /// <summary>
        /// 根据ID查找用户信息数组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<FindHandlerUserOutInfo> FindById(int id, int userId, string connectionId = null, BasicUserInfo currUser = null);
    }
}
