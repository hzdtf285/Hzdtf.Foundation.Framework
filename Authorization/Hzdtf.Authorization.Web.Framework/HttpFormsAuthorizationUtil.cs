using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Hzdtf.Authorization.Web.Framework
{
    /// <summary>
    /// Http表单授权辅助类
    /// @ 黄振东
    /// </summary>
    public static class HttpFormsAuthorizationUtil
    {
        /// <summary>
        /// 设置授权信息到Cookie里
        /// </summary>
        /// <typeparam name="UserDataT">用户数据</typeparam>
        /// <param name="userIdentity">用户标识</param>
        /// <param name="userData">用户数据</param>
        /// <param name="expireMinute">过期分钟</param>
        public static void SetAuthenticationCookie<UserDataT>(UserDataT userData, string userIdentity = UserIdentity.DEFAULT_USER_IDENTITY, int? expireMinute = null)
        {
            if (string.IsNullOrWhiteSpace(userIdentity))
            {
                throw new ArgumentNullException("用户标识不能为空");
            }

            DateTime expireTime = expireMinute == null ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(expireMinute.GetValueOrDefault());
            string userJson = JsonUtil.SerializeIgnoreNull(userData);
            var ticket = new FormsAuthenticationTicket(2, userIdentity, DateTime.Now, DateTime.Now.AddDays(30), false, userJson);

            string encryptValue = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptValue);
            cookie.HttpOnly = true;
            cookie.Domain = FormsAuthentication.CookieDomain;
            if (expireMinute != null)
            {
                cookie.Expires = expireTime;
            }

            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <typeparam name="UserDataT">用户数据类型</typeparam>
        /// <param name="userIdentity">用户标识</param>
        /// <returns>是否已授权</returns>
        public static bool IsAuthenticated<UserDataT>(string userIdentity = UserIdentity.DEFAULT_USER_IDENTITY)
        {
            if (IsAuthenticatedFromContext())
            {
                return true;
            }
            ParseUserData<UserDataT>(userIdentity);

            return IsAuthenticatedFromContext();
        }

        /// <summary>
        /// 从Http请求里获取用户数据
        /// </summary>
        /// <typeparam name="UserDataT">用户数据</typeparam>
        /// <param name="userIdentity">用户标识</param>
        /// <returns>用户数据</returns>
        public static UserDataT ParseUserData<UserDataT>(string userIdentity = UserIdentity.DEFAULT_USER_IDENTITY)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return default(UserDataT);
            }

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            UserDataT userData = ticket.UserData.DeserializeFromJson<UserDataT>();
            if (userData == null)
            {
                return userData;
            }

            GenericPrincipal prin = new GenericPrincipal(new UserIdentity(true, userIdentity), null);
            HttpContext.Current.User = prin;

            return userData;
        }

        /// <summary>
        /// 从上下文中判断是否已授权
        /// </summary>
        /// <returns>是否已授权</returns>
        public static bool IsAuthenticatedFromContext()
        {
            return HttpContext.Current == null
                || HttpContext.Current.User == null
                || HttpContext.Current.User.Identity == null
                ? false
                : HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 从Http请求里移除授权
        /// </summary>
        /// <returns>是否移除成功</returns>
        public static bool RemoveAuthenticate()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now;
            }
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }

            return true;
        }
    }
}
