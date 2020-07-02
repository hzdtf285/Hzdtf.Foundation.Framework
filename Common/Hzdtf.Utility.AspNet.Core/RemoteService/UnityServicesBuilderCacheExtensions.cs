using Hzdtf.Utility.Standard.RemoteService;
using Hzdtf.Utility.Standard.RemoteService.Builder;
using Hzdtf.Utility.Standard.RemoteService.Options;
using Hzdtf.Utility.Standard.RemoteService.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// <param name="options">配置回调</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddUnityServicesBuilderCache(this IServiceCollection services, Action<UnitServiceBuilderOptions> options = null)
        {
            return services.AddUnityServicesBuilder(builderOptions =>
            {
                if (builderOptions.ServicesOptions == null)
                {
                    return new UnityServicesOptionsCache(builderOptions.ServiceBuilderConfigJsonFile);
                }
                else
                {
                    return new UnityServicesOptionsCache(builderOptions.ServicesOptions);
                }
            }, options);
        }

        /// <summary>
        /// 添加统一服务生成器微软配置
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="options">配置回调</param>
        /// <param name="beforeConfigBuilder">生成配置前回调</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddUnityServicesBuilderConfigure(this IServiceCollection services, Action<UnitServiceBuilderOptions> options = null, Action<IConfigurationBuilder> beforeConfigBuilder = null)
        {
            return services.AddUnityServicesBuilder(builderOptions =>
            {
                UnityServicesOptionsConfiguration service = null;
                if (builderOptions.ServicesOptions == null)
                {
                    service = new UnityServicesOptionsConfiguration(builderOptions.ServiceBuilderConfigJsonFile, beforeConfigBuilder);
                }
                else
                {
                    service = new UnityServicesOptionsConfiguration(builderOptions.ServicesOptions, beforeConfigBuilder);
                }

                return service;
            }, options);
        }

        /// <summary>
        /// 添加统一服务生成器
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="callbackServiceOptions">回调服务配置</param>
        /// <param name="options">配置回调</param>
        /// <returns>服务</returns>
        private static IServiceCollection AddUnityServicesBuilder(this IServiceCollection services, Func<UnitServiceBuilderOptions, IUnityServicesOptions> callbackServiceOptions, Action<UnitServiceBuilderOptions> options = null)
        {
            var builderOptions = new UnitServiceBuilderOptions();
            if (options != null)
            {
                options(builderOptions);
            }

            if (builderOptions.UnityServicesOptions == null)
            {
                builderOptions.UnityServicesOptions = callbackServiceOptions(builderOptions);
            }

            services.AddSingleton<IUnityServicesOptions>(builderOptions.UnityServicesOptions);
            if (builderOptions.UnityServicesBuilder == null)
            {
                services.AddSingleton<IUnityServicesBuilder, UnityServicesBuilder>();
            }
            else
            {
                services.AddSingleton<IUnityServicesBuilder>(builderOptions.UnityServicesBuilder);
            }
            if (builderOptions.ServiceProvider == null)
            {
                services.AddSingleton<IServicesProvider, ServicesProviderMemory>();
            }
            else
            {
                services.AddSingleton<IServicesProvider>(builderOptions.ServiceProvider);
            }
            
            return services;
        }
    }
}
