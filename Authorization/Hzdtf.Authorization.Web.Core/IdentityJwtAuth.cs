using Hzdtf.Utility.Standard.Attr;
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
using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Utility.Standard.Data;
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
    /// Jwt:Expires:过期时间，单位为分钟，如未设置，默认为60分钟
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户类型</typeparam>
    [Inject]
    public class IdentityJwtAuth<UserT> : IIdentityTokenAuth, IIdentityAuthVali, IReader<ReturnInfo<UserT>>
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

        /// <summary>
        /// 用户验证
        /// </summary>
        public IUserVali<UserT> UserVali
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public IAuthUserData<UserT> AuthUserData
        {
            get;
            set;
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        #endregion

        #region IReader<IdentityInfoT> 接口

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
                var claims = HttpContextAccessor.HttpContext.User.Claims;
                if (claims == null)
                {
                    return returnInfo;
                }

                returnInfo.Data = IdentityAuthUtil.GetUserDataFromClaims<UserT>(claims, AuthUserData);
            }

            return returnInfo;
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
            if (UserVali == null)
            {
                throw new NullReferenceException("用户验证不能为null");
            }

            var re = new ReturnInfo<string>();
            ReturnInfo<UserT> returnInfo = UserVali.Vali(user, password);
            re.FromBasic(returnInfo);
            if (re.Failure())
            {
                return re;
            }

            var claims = IdentityAuthUtil.SaveUserInfoGetClaims(returnInfo.Data, AuthUserData);
            re.Data = BuilderToken(claims);

            return re;
        }

        #endregion

        #region IIdentityAuthVali 接口

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed()
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

        #region 私有方法

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="claims">证件单元集合</param>
        /// <returns>令牌</returns>
        private string BuilderToken(IList<Claim> claims)
        {
            var domain = AppConfig["Jwt:Domain"];
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentNullException("Jwt域名不能为空");
            }
            var securityKey = AppConfig["Jwt:SecurityKey"];
            if (string.IsNullOrWhiteSpace(securityKey))
            {
                throw new ArgumentNullException("Jwt密钥不能为空");
            }

            var expiresStr = AppConfig["Jwt:Expires"];
            var expires = string.IsNullOrWhiteSpace(expiresStr) ? 60 : Convert.ToInt32(expiresStr);

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
