using Hzdtf.Authorization.Web.Framework;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hzdtf.BasicFunction.MvcController.Framework.Home
{
    /// <summary>
    /// 授权控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Authorize]
    public class AuthorizationController : System.Web.Mvc.Controller
    {
        /// <summary>
        /// 身份授权
        /// </summary>
        public IdentityHttpFormAuth IdentityAuth
        {
            get;
            set;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>返回信息</returns>
        [HttpPost]
        [AllowAnonymous]
        public virtual ReturnInfo<LoginReturnInfo> Login(LoginInfo user)
        {
            ReturnInfo<LoginReturnInfo> returnInfo = new ReturnInfo<LoginReturnInfo>()
            {
                Data = new LoginReturnInfo()
            };
            
            ushort num = 0;
            if (Session["ErrLoginNum"] != null)
            {
                num = Convert.ToUInt16(Session["ErrLoginNum"]);
            }
            num++;
            // 错误登录超过3次则需要验证码
            if (num > 3)
            {
                if (string.IsNullOrWhiteSpace(user.VerificationCode))
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("请输入验证码");

                    return returnInfo;
                }

                if (string.Compare(user.VerificationCode, Session["VerificationCode"].ToString(), true) != 0)
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("验证码不对，请输入正确的验证码");

                    return returnInfo;
                }
            }

            ReturnInfo<BasicUserInfo> userReturnInfo = IdentityAuth.Accredit(user.LoginId, user.Password);
            returnInfo.FromBasic(userReturnInfo);
            if (returnInfo.Failure() || userReturnInfo.Data == null)
            {
                Session["ErrLoginNum"] = num;
                returnInfo.Data.IsVerificationCode = IsNeedIsVerificationCode();
            }
            else
            {
                Session.Remove("VerificationCode");
                Session.Remove("ErrLoginNum");
            }

            return returnInfo;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpDelete]
        public virtual ReturnInfo<bool> Logout()
        {
            ReturnInfo<bool> returnInfo = IdentityAuth.Exit();
            if (returnInfo.Success())
            {
                Session.Clear();
            }

            return returnInfo;
        }

        /// <summary>
        /// 获取是否需要验证码
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet]
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
        private bool IsNeedIsVerificationCode()
        {
            if (Session["ErrLoginNum"] == null)
            {
                return false;
            }

            return Convert.ToUInt16(Session["ErrLoginNum"]) > 2;
        }
    }
}
