using Hzdtf.Utility.Standard.RemoteService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.RemoteService
{
    /// <summary>
    /// 统一服务生成器缓存扩展类
    /// @ 黄振东
    /// </summary>
    public static class UnityServicesBuilderCacheExtensions
    {
        /// <summary>
        /// 添加统一服务生成器缓存
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="unityServicesOptionsJsonFile">统一服务选项配置JSON文件</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddUnityServicesBuilderCache(this IServiceCollection services, string unityServicesOptionsJsonFile = "Config/serviceBuilderConfig.json")
        {
            var config = new ConfigurationBuilder().AddJsonFile(unityServicesOptionsJsonFile).Build();
            services.Configure<UnityServicesOptions>(config);

            return services;
        }

        /// <summary>
        /// 使用统一服务生成器缓存
        /// </summary>
        /// <param name="app">应用</param>
        /// <param name="config">配置</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseUnityServicesBuilderCache(this IApplicationBuilder app, Action<UnityServicesOptions> config = null)
        {
            // 获取主机的生命周期管理对象
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            // 获取服务配置对象
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<UnityServicesOptions>>().Value;
            if (config != null)
            {
                config(serviceOptions);
            }

            UnityServicesBuilderCache.SetOptions(serviceOptions);

            return app;
        }
    }
}
