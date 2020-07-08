using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Rabbit.Impl.Standard;
using Hzdtf.Rabbit.Impl.Standard.Connection;

namespace Hzdtf.Rabbit.AspNet.Core
{
    /// <summary>
    /// Rabbit连接来自微软配置默认工厂
    /// @ 黄振东
    /// </summary>
    public static class RabbitConnectionConfigureDefaultFactory
    {
        /// <summary>
        /// 创建消息队列连接来自微软配置并打开
        /// </summary>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigBuilder">生成配置前回调</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionConfigureAndOpen(Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null, Action<IConfigurationBuilder, string, object> beforeConfigBuilder = null)
        {
            var factory = new RabbitConnectionFactory();

            return CreateConnectionConfigureAndOpen(factory, funQueueReader, options, beforeConfigBuilder);
        }

        /// <summary>
        /// 创建消息队列连接来自微软配置并打开
        /// </summary>
        /// <param name="factory">Rabbit连接工厂</param>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigBuilder">生成配置前回调</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionConfigureAndOpen(out IMessageQueueConnectionFactory factory, Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null, Action<IConfigurationBuilder, string, object> beforeConfigBuilder = null)
        {
            factory = new RabbitConnectionFactory();

            return CreateConnectionConfigureAndOpen(factory, funQueueReader, options, beforeConfigBuilder);
        }

        /// <summary>
        /// 创建消息队列连接来自微软配置并打开
        /// </summary>
        /// <param name="factory">Rabbit连接工厂</param>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigBuilder">生成配置前回调</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionConfigureAndOpen(this IMessageQueueConnectionFactory factory, Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null, Action<IConfigurationBuilder, string, object> beforeConfigBuilder = null)
        {
            return factory.CreateConnectionAndOpen(jsonFile =>
            {
                return new RabbitMessageQueueConfiguration(jsonFile, beforeConfigBuilder);
            }, options);
        }
    }
}
