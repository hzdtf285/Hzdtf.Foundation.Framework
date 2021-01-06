using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;

namespace Hzdtf.Authorization.Contract.Standard.User
{
    /// <summary>
    /// 用户验证接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public interface IUserVali<IdT, UserT> where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        ReturnInfo<UserT> Vali(string user, string password);
    }
}
