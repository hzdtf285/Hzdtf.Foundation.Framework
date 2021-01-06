using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.RequestLog
{
    /// <summary>
    /// 请求日志扩展类
    /// @ 黄振东
    /// </summary>
    public static class RequestLogServiceExtensions
    {
        /// <summary>
        /// 添加请求日志服务
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">请求日志选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddRequestLog(this IServiceCollection services, Action<RequestLogOptions> options = null)
        {
            var apiExceptionHandleOptions = new RequestLogOptions();
            if (options != null)
            {
                options(apiExceptionHandleOptions);
            }

            services.AddSingleton<IOptions<RequestLogOptions>>(provider =>
            {
                return Options.Create<RequestLogOptions>(apiExceptionHandleOptions);
            });

            return services;
        }
    }
}
