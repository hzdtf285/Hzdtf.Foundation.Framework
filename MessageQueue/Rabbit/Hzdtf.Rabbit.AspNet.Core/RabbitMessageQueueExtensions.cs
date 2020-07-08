using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
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
        public static IMessageQueueConnection AddRabbit(this IServiceCollection services, Action<RabbitConnectionWrapInfo> options = null)
        {
            return services.AddRabbitDesc(jsonFile =>
            {
                return new RabbitMessageQueueJson(jsonFile);
            }, options);
        }

        /// <summary>
        /// 添加Rabbit微软配置
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="options">配置回调</param>
        /// <param name="beforeConfigBuilder">生成配置前回调</param>
        /// <returns>服务</returns>
        public static IMessageQueueConnection AddRabbitConfigure(this IServiceCollection services, Action<RabbitConnectionWrapInfo> options = null, Action<IConfigurationBuilder, string, object> beforeConfigBuilder = null)
        {
            return services.AddRabbitDesc(jsonFile =>
            {
                return new RabbitMessageQueueConfiguration(jsonFile, beforeConfigBuilder);
            }, options);
        }

        /// <summary>
        /// 添加Rabbit详细
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="funQueueReader">消息队列读取回调</param>
        /// <param name="options">配置回调</param>
        /// <returns>消息队列连接</returns>
        private static IMessageQueueConnection AddRabbitDesc(this IServiceCollection services, Func<string, IRabbitMessageQueueReader> funQueueReader, Action<RabbitConnectionWrapInfo> options = null)
        {
            IMessageQueueConnectionFactory factoy;
            var conn = RabbitConnectionDefaultFactory.CreateConnectionAndOpen(out factoy, funQueueReader, options);

            services.AddSingleton<IMessageQueueConnectionFactory>(factoy);
            services.AddSingleton<IMessageQueueConnection>(conn);

            return conn;
        }
    }
}
