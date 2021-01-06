using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Identitys;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard
{
    /// <summary>
    /// 身份授权辅助类
    /// @ 黄振东
    /// </summary>
    public static class IdentityAuthUtil
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <typeparam name="IdT">Id类型</typeparam>
        /// <typeparam name="IdentityInfoT">身份模型类型</typeparam>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="userAlias">用户别名</param>
        /// <returns>是否验证通过</returns>
        public static bool ValiParams<IdT, IdentityInfoT>(ReturnInfo<IdentityInfoT> returnInfo, string user, string password, string userAlias = null)
        where IdentityInfoT : BasicUserInfo<IdT>
        {            
            if (string.IsNullOrWhiteSpace(user))
            {
                if (string.IsNullOrWhiteSpace(userAlias))
                {
                    userAlias = "用户";
                }
                returnInfo.SetFailureMsg($"请输入{userAlias}");

                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                returnInfo.SetFailureMsg("请输入密码");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存用户信息并获取证件单元集合
        /// </summary>
        /// <typeparam name="IdT">ID类型</typeparam>
        /// <typeparam name="UserT">用户类型</typeparam>
        /// <param name="user">用户信息</param>
        /// <param name="authUserData">身份用户数据</param>
        /// <returns>证件单元集合</returns>
        public static IList<Claim> SaveUserInfoGetClaims<IdT, UserT>(UserT user, IAuthUserData<IdT, UserT> authUserData)
            where UserT : BasicUserInfo<IdT>
        {
            if (authUserData == null)
            {
                throw new NullReferenceException("身份用户数据(authUserData)不能为null");
            }

            var claims = new List<Claim>(20);
            claims.Add(ClaimTypes.NameIdentifier, user.Id.ToString());
            claims.Add(ClaimTypes.Name, user.Name);
            claims.Add("code", user.Code);
            claims.Add("loginId", user.LoginId);
            claims.Add("loginTime", DateTime.Now.ToFullFixedDateTime());
            claims.Add("tenantId", user.TenantId.ToString());

            authUserData.SetExtraToClaimsData(claims, user);

            return claims;
        }

        /// <summary>
        /// 从证件单元集合里获取用户数据
        /// </summary>
        /// <typeparam name="IdT">ID类型</typeparam>
        /// <typeparam name="UserT">用户类型</typeparam>
        /// <param name="claims">证件单元集合</param>
        /// <param name="authUserData">身份授权用户数据</param>
        /// <returns>用户</returns>
        public static UserT GetUserDataFromClaims<IdT, UserT>(IEnumerable<Claim> claims, IAuthUserData<IdT, UserT> authUserData)
            where UserT : BasicUserInfo<IdT>
        {
            if (authUserData == null)
            {
                throw new NullReferenceException("身份用户数据(authUserData)不能为null");
            }

            var user = authUserData.CreateUser();
            var identity = AutofacTool.Resolve<IIdentity<IdT>>();
            user.Id = identity.ConvertTo(claims.Get(ClaimTypes.NameIdentifier));
            user.Name = claims.Get(ClaimTypes.Name);
            user.Code = claims.Get("code");
            user.LoginId = claims.Get("loginId");
            user.TenantId = identity.ConvertTo(claims.Get("tenantId"));

            var loginTimeStr = claims.Get("loginTime");
            if (!string.IsNullOrWhiteSpace(loginTimeStr))
            {
                DateTime loginTime;
                if (DateTime.TryParse(loginTimeStr, out loginTime))
                {
                    user.LoginTime = loginTime;
                }
            }

            authUserData.SetExtraToUserData(user, claims);

            return user;
        }
    }
}
