using Hzdtf.Authorization.Web.Core;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Hzdtf.BasicFunction.MvcController.Core.Home
{
    /// <summary>
    /// 授权控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// 身份授权
        /// </summary>
        public IdentityCookieAuth IdentityAuth
        {
            get;
            set;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>返回信息</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public ReturnInfo<LoginReturnInfo> Login(LoginInfo loginInfo)
        {
            ReturnInfo<LoginReturnInfo> returnInfo = new ReturnInfo<LoginReturnInfo>()
            {
                Data = new LoginReturnInfo()
            };

            int num = HttpContext.Session.GetInt32("ErrLoginNum").GetValueOrDefault();
            num++;
            //错误登录超过3次则需要验证码
            if (num > 3)
            {
                if (string.IsNullOrWhiteSpace(loginInfo.VerificationCode))
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("请输入验证码");

                    return returnInfo;
                }

                if (string.Compare(loginInfo.VerificationCode, HttpContext.Session.GetString("VerificationCode"), true) != 0)
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("验证码不对，请输入正确的验证码");

                    return returnInfo;
                }
            }

            ReturnInfo<BasicUserInfo> userReturnInfo = IdentityAuth.Accredit(loginInfo.LoginId, loginInfo.Password);
            returnInfo.FromBasic(userReturnInfo);
            if (returnInfo.Failure() || userReturnInfo.Data == null)
            {
                HttpContext.Session.SetInt32("ErrLoginNum", num);
                returnInfo.Data.IsVerificationCode = IsNeedIsVerificationCode();
            }
            else
            {
                HttpContext.Session.Remove("VerificationCode");
                HttpContext.Session.Remove("ErrLoginNum");
            }

            return returnInfo;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpDelete("Logout")]
        public ReturnInfo<bool> Logout()
        {
            ReturnInfo<bool> returnInfo = IdentityAuth.Exit();
            if (returnInfo.Success())
            {
                HttpContext.Session.Clear();
            }

            return returnInfo;
        }

        /// <summary>
        /// 获取是否需要验证码
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("GetIsVerificationCode")]
        [AllowAnonymous]
        public ReturnInfo<bool> GetIsVerificationCode()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = IsNeedIsVerificationCode();

            return returnInfo;
        }

        /// <summary>
        /// 判断是否需要验证码
        /// </summary>
        /// <returns>是否需要验证码</returns>
        private bool IsNeedIsVerificationCode() => HttpContext.Session.GetInt32("ErrLoginNum").GetValueOrDefault() > 2;
    }
}
