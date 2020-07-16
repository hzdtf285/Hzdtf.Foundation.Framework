using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 身份认证辅助类
    /// @ 黄振东
    /// </summary>
    public static class AuthUtil
    {
        /// <summary>
        /// 授权key，值是：Authorization
        /// </summary>
        public const string AUTH_KEY = "Authorization";

        /// <summary>
        /// 添加Bearer票据
        /// </summary>
        /// <param name="originalToken">原始票据</param>
        /// <returns>添加Bearer票据后的票据</returns>
        public static string AddBearerToken(this string originalToken)
        {
            return IsContainerBearer(originalToken) ? originalToken : $"bearer {originalToken}";
        }

        /// <summary>
        /// 获取Beaer原始票据
        /// </summary>
        /// <param name="bearerToken">带有Bearer的票据</param>
        /// <returns>Beaer原始票据</returns>
        public static string GetBearerOriginalToken(this string bearerToken)
        {
            return IsContainerBearer(bearerToken) ? bearerToken.Substring(7) : bearerToken;
        }

        /// <summary>
        /// 判断票据是否包含了Bearer
        /// </summary>
        /// <param name="token">票据</param>
        /// <returns>票据是否包含了Bearer</returns>
        private static bool IsContainerBearer(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length < 7)
            {
                return false;
            }

            return "bearer ".Equals(token.Substring(0, 7).ToLower());
        }
    }
}
