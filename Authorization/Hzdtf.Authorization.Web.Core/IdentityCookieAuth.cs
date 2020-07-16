using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Hzdtf.Utility.Standard.Model;
using System.Collections.Generic;
using System;
using Hzdtf.Authorization.Contract.Standard.IdentityAuth;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份Cookie授权
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户类型</typeparam>
    [Inject]
    public class IdentityCookieAuth<UserT> : IdentityClaimAuthBase<UserT>, IIdentityExit
        where UserT : BasicUserInfo
    {
        #region 属性与字段

        /// <summary>
        /// Http上下文访问
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor
        {
            get;
            set;
        }

        #endregion

        #region IIdentityAuthVali 接口

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> IsAuthed()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            if (HttpContextAccessor != null && HttpContextAccessor.HttpContext != null 
                && HttpContextAccessor.HttpContext.User != null && HttpContextAccessor.HttpContext.User.Identity != null
                && HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                returnInfo.Data = true;
            }

            return returnInfo;
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
            HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            returnInfo.SetSuccessMsg("退出成功");

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 获取证件单元集合
        /// </summary>
        /// <returns>证件单元集合</returns>
        protected override IEnumerable<Claim> GetClaims() => HttpContextAccessor.HttpContext.User.Claims;

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
            HttpContextAccessor.HttpContext.SignInAsync(principal);
        }

        #endregion
    }
}
