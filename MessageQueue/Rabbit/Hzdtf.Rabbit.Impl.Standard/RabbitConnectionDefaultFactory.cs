using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard.Connection;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard
{
    /// <summary>
    /// Rabbit连接默认工厂
    /// @ 黄振东
    /// </summary>
    public static class RabbitConnectionDefaultFactory
    {
        /// <summary>
        /// 创建消息队列连接并打开
        /// </summary>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionAndOpen(Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null)
        {
            var factory = new RabbitConnectionFactory();

            return CreateConnectionAndOpen(factory, funQueueReader, options);
        }

        /// <summary>
        /// 创建消息队列连接并打开
        /// </summary>
        /// <param name="factory">Rabbit连接工厂</param>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionAndOpen(out IMessageQueueConnectionFactory factory, Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null)
        {
            factory = new RabbitConnectionFactory();

            return CreateConnectionAndOpen(factory, funQueueReader, options);
        }

        /// <summary>
        /// 创建消息队列连接并打开
        /// </summary>
        /// <param name="factory">Rabbit连接工厂</param>
        /// <param name="funQueueReader">消息队列读取</param>
        /// <param name="options">配置</param>
        /// <returns>消息队列连接</returns>
        public static IMessageQueueConnection CreateConnectionAndOpen(this IMessageQueueConnectionFactory factory, Func<string, IRabbitMessageQueueReader> funQueueReader = null, Action<RabbitConnectionWrapInfo> options = null)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("Rabbit连接工厂不能为null");
            }
            var config = new RabbitConnectionWrapInfo();
            if (options != null)
            {
                options(config);
            }

            var conn = factory.CreateAndOpen(config);
            IRabbitMessageQueueReader ququeReader = null;
            if (funQueueReader == null)
            {
                ququeReader = new RabbitMessageQueueJson(config.MessageQueueJsonFile);
            }
            else
            {
                ququeReader = funQueueReader(config.MessageQueueJsonFile);
            }
            conn.SetMessageQueueReader(ququeReader);

            return conn;
        }
    }
}
