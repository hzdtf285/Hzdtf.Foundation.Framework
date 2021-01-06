using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.RequestLog
{
    /// <summary>
    /// 请求日志中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class RequestLogMiddlewareExtensions
    {
        /// <summary>
        /// 使用请求日志
        /// </summary>
        /// <param name="app">应用</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<RequestLogOptions>>().Value;
            app.UseMiddleware<RequestLogMiddleware>();

            return app;
        }
    }
}
