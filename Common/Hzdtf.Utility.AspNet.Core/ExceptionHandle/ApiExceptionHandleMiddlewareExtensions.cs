using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.ExceptionHandle
{
    /// <summary>
    /// Api异常处理中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class ApiExceptionHandleMiddlewareExtensions
    {
        /// <summary>
        /// 使用Api异常处理
        /// </summary>
        /// <param name="app">应用</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseApiExceptionHandle(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<ApiExceptionHandleOptions>>().Value;
            var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
            options.IsDevelopment = env.IsDevelopment();

            app.UseMiddleware<ApiExceptionHandleMiddleware>();

            return app;
        }
    }
}
