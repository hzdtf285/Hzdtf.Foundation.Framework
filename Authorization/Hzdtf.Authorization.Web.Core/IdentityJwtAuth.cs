using Hzdtf.Utility.Standard.Model.Return;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Hzdtf.Authorization.Contract.Standard.IdentityAuth.Token;
using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份Jwt授权
    /// 相差配置请在appsetting.json里设置，以Jwt为根
    /// Jwt:Domain:域名，不可为空
    /// Jwt:SecurityKey:密钥，不可为空
    /// Jwt:Expires:过期时间，单位为分钟，如未设置，默认为2小时
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public class IdentityJwtAuth<IdT, UserT> : IIdentityTokenAuth
        where UserT : BasicUserInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// Http上下文访问
        /// </summary>
        private readonly IHttpContextAccessor httpContext;

        /// <summary>
        /// 用户验证
        /// </summary>
        private readonly IUserVali<IdT, UserT> userVali;

        /// <summary>
        /// 授权用户数据
        /// </summary>
        public readonly IAuthUserData<IdT, UserT> authUserData;

        /// <summary>
        /// 应用配置
        /// </summary>
        public readonly IAppConfiguration appConfig;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userVali">用户验证</param>
        /// <param name="authUserData">授权用户数据</param>
        /// <param name="httpContext">Http上下文访问</param>
        /// <param name="appConfig">应用配置</param>
        public IdentityJwtAuth(IUserVali<IdT, UserT> userVali, IAuthUserData<IdT, UserT> authUserData, IHttpContextAccessor httpContext, IAppConfiguration appConfig)
        {
            this.userVali = userVali;
            this.httpContext = httpContext;
            this.authUserData = authUserData;
            if (appConfig == null)
            {
                this.appConfig = PlatformTool.AppConfig;
            }
            else
            {
                this.appConfig = appConfig;
            }
        }

        #endregion

        #region IIdentityTokenAuth

        /// <summary>
        /// 授权并生成令牌
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<string> AccreditToToken(string user, string password)
        {
            if (userVali == null)
            {
                throw new NullReferenceException("用户验证不能为null");
            }

            var re = new ReturnInfo<string>();
            ReturnInfo<UserT> returnInfo = userVali.Vali(user, password);
            re.FromBasic(returnInfo);
            if (re.Failure())
            {
                return re;
            }

            var claims = IdentityAuthUtil.SaveUserInfoGetClaims(returnInfo.Data, authUserData);
            re.Data = BuilderToken(claims);

            return re;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="claims">证件单元集合</param>
        /// <returns>令牌</returns>
        private string BuilderToken(IList<Claim> claims)
        {
            var domain = appConfig["Jwt:Domain"];
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentNullException("Jwt域名不能为空");
            }
            var securityKey = appConfig["Jwt:SecurityKey"];
            if (string.IsNullOrWhiteSpace(securityKey))
            {
                throw new ArgumentNullException("Jwt密钥不能为空");
            }

            var expiresStr = appConfig["Jwt:Expires"];
            var expires = string.IsNullOrWhiteSpace(expiresStr) ? 120 : Convert.ToInt32(expiresStr);

            claims.Add(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}");
            claims.Add(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(expires)).ToUnixTimeSeconds()}");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: domain,
                audience: domain,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
