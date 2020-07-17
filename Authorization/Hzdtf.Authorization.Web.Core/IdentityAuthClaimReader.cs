using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份认证证件单元读取
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户类型</typeparam>
    public class IdentityAuthClaimReader<UserT> : IIdentityAuthReader<UserT>
        where UserT : BasicUserInfo
    {
        /// <summary>
        /// Http上下文访问
        /// </summary>
        private readonly IHttpContextAccessor httpContext;

        /// <summary>
        /// 授权用户数据
        /// </summary>
        private readonly IAuthUserData<UserT> authUserData;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpContext">Http上下文访问</param>
        /// <param name="authUserData">授权用户数据</param>
        public IdentityAuthClaimReader(IHttpContextAccessor httpContext, IAuthUserData<UserT> authUserData)
        {
            this.httpContext = httpContext;
            this.authUserData = authUserData;
        }

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            if (httpContext != null && httpContext.HttpContext != null
                && httpContext.HttpContext.User != null && httpContext.HttpContext.User.Identity != null
                && httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                returnInfo.Data = true;
            }

            return returnInfo;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<UserT> Reader()
        {
            ReturnInfo<UserT> returnInfo = new ReturnInfo<UserT>();
            ReturnInfo<bool> isAuthReturnInfo = IsAuthed();
            if (isAuthReturnInfo.Success() && isAuthReturnInfo.Data)
            {
                var claims = httpContext.HttpContext.User.Claims;
                if (claims == null)
                {
                    return returnInfo;
                }

                returnInfo.Data = IdentityAuthUtil.GetUserDataFromClaims<UserT>(claims, authUserData);
            }

            return returnInfo;
        }
    }
}
