using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.ExceptionHandle
{
    /// <summary>
    /// Api异常处理服务扩展类
    /// @ 黄振东
    /// </summary>
    public static class ApiExceptionHandleServiceExtensions
    {
        /// <summary>
        /// 添加Api异常处理服务
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">Api异常处理选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddApiExceptionHandle(this IServiceCollection services, Action<ApiExceptionHandleOptions> options = null)
        {
            var apiExceptionHandleOptions = new ApiExceptionHandleOptions();
            if (options != null)
            {
                options(apiExceptionHandleOptions);
            }

            services.AddSingleton<IOptions<ApiExceptionHandleOptions>>(provider =>
            {
                return Options.Create<ApiExceptionHandleOptions>(apiExceptionHandleOptions);
            });

            return services;
        }
    }
}
