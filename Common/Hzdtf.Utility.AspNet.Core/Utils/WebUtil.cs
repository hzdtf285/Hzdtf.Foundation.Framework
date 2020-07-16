using Hzdtf.Utility.Standard.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.Utils
{
    /// <summary>
    /// Web 辅助类
    /// @ 黄振东
    /// </summary>
    public static class WebUtil
    {
        /// <summary>
        /// 从请求头里获取原始的票据
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>原始的票据</returns>
        public static string GetBearerOriginTokenFromHeader(this HttpRequest request)
        {
            var containerBearerToken = request.GetContainerBearerOriginTokenFromHeader();

            return containerBearerToken.GetBearerOriginalToken();
        }

        /// <summary>
        /// 从请求头里获取带有Bearer的票据
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>带有Bearer的票据</returns>
        public static string GetContainerBearerOriginTokenFromHeader(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey(AuthUtil.AUTH_KEY))
            {
                return null;
            }

            return request.Headers[AuthUtil.AUTH_KEY].ToString();
        }
    }
}
