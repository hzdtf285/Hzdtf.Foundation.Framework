using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Hzdtf.Utility.Standard.Model;
using System.Collections.Generic;
using System;
using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Authorization.Contract.Standard.User;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份Cookie授权
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public class IdentityCookieAuth<IdT, UserT> : IdentityClaimAuthBase<IdT, UserT>, IIdentityExit
        where UserT : BasicUserInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// Http上下文访问
        /// </summary>
        private readonly IHttpContextAccessor httpContext;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userVali">用户验证</param>
        /// <param name="authUserData">授权用户数据</param>
        /// <param name="httpContext">Http上下文访问</param>
        public IdentityCookieAuth(IUserVali<IdT, UserT> userVali, IAuthUserData<IdT, UserT> authUserData, IHttpContextAccessor httpContext)
            : base(userVali, authUserData)
        {
            this.httpContext = httpContext;
        }

        #endregion

        #region IIdentityExit 接口

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Exit()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            returnInfo.SetSuccessMsg("退出成功");

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 获取证件单元集合
        /// </summary>
        /// <returns>证件单元集合</returns>
        protected override IEnumerable<Claim> GetClaims() => httpContext.HttpContext.User.Claims;

        /// <summary>
        /// 获取身份认证方案
        /// </summary>
        /// <returns>身份认证方案</returns>
        protected override string GetAuthenticationScheme() => CookieAuthenticationDefaults.AuthenticationScheme;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="principal">当事人</param>
        protected override void SignIn(ClaimsPrincipal principal)
        {
            httpContext.HttpContext.SignInAsync(principal);
        }

        #endregion
    }
}
