using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard;
using Hzdtf.Utility.Standard.RemoteService;
using Hzdtf.Utility.Standard.RemoteService.Builder;
using Hzdtf.Utility.Standard.RemoteService.Options;
using Hzdtf.Utility.Standard.RemoteService.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.AspNet.Core
{
    /// <summary>
    /// Rabbit消息队列扩展类
    /// @ 黄振东
    /// </summary>
    public static class RabbitMessageQueueExtensions
    {
        /// <summary>
        /// 添加Rabbit
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="options">配置回调</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddRabbit(this IServiceCollection services, Action<RabbitOptions> options = null)
        {
            var config = new RabbitOptions();
            if (options != null)
            {
                options(config);
            }

            var conn = RabbitConnectionFactory.CreateAndOpen(config.ConnectionStringConfigName, config.MessageQueueFilePath, config.ContentType);
            services.AddSingleton<IMessageQueueConnection>(conn);

            return services;
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
                //UnityServicesOptionsConfiguration service = null;
                //if (builderOptions.ServicesOptions == null)
                //{
                //    service = new UnityServicesOptionsConfiguration(builderOptions.ServiceBuilderConfigJsonFile, beforeConfigBuilder);
                //}
                //else
                //{
                //    service = new UnityServicesOptionsConfiguration(builderOptions.ServicesOptions, beforeConfigBuilder);
                //}

                //return service;

                return null;
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
