using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard;
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
    }
}
