using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.User
{
    /// <summary>
    /// 身份认证用户数据接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">Id类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public interface IAuthUserData<IdT, UserT> where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns>用户</returns>
        UserT CreateUser();
        
        /// <summary>
        /// 设置额外的用户数据
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="claims">证件单元集合</param>
        void SetExtraToUserData(UserT user, IEnumerable<Claim> claims);

        /// <summary>
        /// 设置额外的证件单元集合
        /// </summary>
        /// <param name="claims">用户</param>
        /// <param name="user">证件单元集合</param>
        void SetExtraToClaimsData(IList<Claim> claims, UserT user);
    }
}
