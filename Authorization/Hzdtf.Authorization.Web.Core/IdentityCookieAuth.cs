using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Hzdtf.Utility.Standard.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份Cookie授权
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IdentityCookieAuth : IdentityAuthBase, IIdentityAuthVali, IIdentityExit, IReader<ReturnInfo<BasicUserInfo>>
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

        #region IIdentityExit 接口

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Exit()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            Task task = HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            task.Wait();

            returnInfo.SetSuccessMsg("退出成功");

            return returnInfo;
        }

        #endregion

        #region IReader<IdentityInfoT> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<BasicUserInfo> Reader()
        {
            ReturnInfo<BasicUserInfo> returnInfo = new ReturnInfo<BasicUserInfo>();
            ReturnInfo<bool> isAuthReturnInfo = IsAuthed();
            if (isAuthReturnInfo.Success() && isAuthReturnInfo.Data)
            {
                BasicUserInfo user = new BasicUserInfo();
                foreach (Claim c in HttpContextAccessor.HttpContext.User.Claims)
                {
                    PropertyInfo p = user.GetType().GetProperty(c.Type);
                    if (p == null || !p.CanWrite)
                    {
                        continue;
                    }

                    p.SetPropertyValue(user, c.Value);
                }

                returnInfo.Data = user;
            }

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="basicUser">基本用户</param>
        protected override void SaveUserInfo(BasicUserInfo basicUser)
        {
            PropertyInfo[] properties = typeof(BasicUserInfo).GetProperties();
            IList<Claim> claims = new List<Claim>(properties.Length);
            foreach (PropertyInfo p in properties)
            {
                if (p.CanRead)
                {
                    object value = p.GetValue(basicUser);
                    if (value == null)
                    {
                        continue;
                    }

                    claims.Add(new Claim(p.Name, value.ToString()));
                }
            }

            var user = new UserIdentity(true, authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);
            var identity = new ClaimsIdentity(user);
            identity.AddClaims(claims);

            Task task = HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            task.Wait();            
        }

        #endregion
    }
}
