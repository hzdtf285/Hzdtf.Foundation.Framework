using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.AspNet.Core.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hzdtf.Rabbit.Impl.Standard;
using Hzdtf.Rabbit.Model.Standard.Connection;

namespace Hzdtf.Rabbit.AspNet.Core
{
    /// <summary>
    /// Rabbit消息队列来自微软配置对象里
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueConfiguration : JsonFileMicrosoftConfigurationBase<RabbitConfigInfo>, IRabbitMessageQueueReader, IRabbitConfigReader
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public RabbitMessageQueueConfiguration(string jsonFile = "Config/rabbitMessageQueue.json", Action<IConfigurationBuilder, string, object> beforeConfigurationBuilder = null) 
            : base(jsonFile, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public RabbitMessageQueueConfiguration(RabbitConfigInfo options, Action<IConfigurationBuilder, string, object> beforeConfigurationBuilder = null)
            : base(options, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 根据队列读取消息队列信息
        /// </summary>
        /// <param name="queueOrOtherIdentify">队列或其他标识</param>
        /// <param name="extend">扩展</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo Reader(string queueOrOtherIdentify, IDictionary<string, string> extend = null)
        {
            return RabbitMessageQueueUtil.Reader(queueOrOtherIdentify, ReaderAll(extend));
        }

        /// <summary>
        /// 读取全部消息队列信息列表
        /// </summary>
        /// <param name="extend">扩展</param>
        /// <returns>全部消息队列信息列表</returns>
        public IList<RabbitMessageQueueInfo> ReaderAll(IDictionary<string, string> extend = null)
        {
            var rabbitConfig = Reader();
            return rabbitConfig.GetExchanges(extend.GetVirtualPath()).ToMessageQueues();
        }

        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByExchange(string exchange, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            return RabbitMessageQueueUtil.ReaderByExchange(exchange, ReaderAll(ConfigUtil.CreateContainerVirtualPathDic(virtualPath)));
        }

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>消息队列信息</returns>
        public RabbitMessageQueueInfo ReaderByQueue(string queue, string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            return RabbitMessageQueueUtil.ReaderByQueue(queue, ReaderAll(ConfigUtil.CreateContainerVirtualPathDic(virtualPath)));
        }
    }
}
