using System.Security.Claims;
using Hzdtf.Utility.Standard.Model;
using System.Collections.Generic;
using System;
using Hzdtf.Authorization.Contract.Standard.User;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth
{
    /// <summary>
    /// 身份证件授权
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户类型</typeparam>
    public abstract class IdentityClaimAuthBase<UserT> : IdentityAuthBase<UserT>
        where UserT : BasicUserInfo
    {
        #region 属性与字段

        /// <summary>
        /// 授权用户数据
        /// </summary>
        private readonly IAuthUserData<UserT> authUserData;

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userVali">用户验证</param>
        /// <param name="authUserData">授权用户数据</param>
        public IdentityClaimAuthBase(IUserVali<UserT> userVali, IAuthUserData<UserT> authUserData)
            : base(userVali)
        {
            this.authUserData = authUserData;
        }

        #region 重写父类的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户</param>
        protected override void SaveUserInfo(UserT user)
        {
            var claims = IdentityAuthUtil.SaveUserInfoGetClaims(user, authUserData);
            var identity = new ClaimsIdentity(claims, GetAuthenticationScheme());

            SignIn(new ClaimsPrincipal(identity));
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取证件单元集合
        /// </summary>
        /// <returns>证件单元集合</returns>
        protected abstract IEnumerable<Claim> GetClaims();

        /// <summary>
        /// 获取身份认证方案
        /// </summary>
        /// <returns>身份认证方案</returns>
        protected abstract string GetAuthenticationScheme();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="principal">当事人</param>
        protected abstract void SignIn(ClaimsPrincipal principal);

        #endregion
    }
}
