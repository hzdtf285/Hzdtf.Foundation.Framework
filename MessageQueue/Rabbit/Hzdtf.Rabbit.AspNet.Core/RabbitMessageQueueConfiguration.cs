using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.AspNet.Core.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hzdtf.Rabbit.AspNet.Core
{
    /// <summary>
    /// Rabbit消息队列来自微软配置对象里
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueConfiguration : JsonFileMicrosoftConfigurationBase<IList<RabbitMessageQueueInfo>>, IRabbitMessageQueueReader
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public RabbitMessageQueueConfiguration(string jsonFile = "Config/rabbitMessageQueue.json", Action<IConfigurationBuilder> beforeConfigurationBuilder = null) 
            : base(jsonFile, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public RabbitMessageQueueConfiguration(IList<RabbitMessageQueueInfo> options, Action<IConfigurationBuilder> beforeConfigurationBuilder = null)
            : base(options, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 根据队列读取消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo Reader(string queueOrOtherIdentify)
        {
            return RabbitMessageQueueUtil.Reader(queueOrOtherIdentify, ReaderAll());
        }

        /// <summary>
        /// 读取全部消息队列信息列表
        /// </summary>
        /// <returns>全部消息队列信息列表</returns>
        public IList<RabbitMessageQueueInfo> ReaderAll() => Reader();

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchange(string exchange)
        {
            return RabbitMessageQueueUtil.ReaderByExchange(exchange, ReaderAll());
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue)
        {
            return RabbitMessageQueueUtil.ReaderByQueue(queue, ReaderAll());
        }
    }
}
