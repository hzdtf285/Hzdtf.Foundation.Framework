using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Contract.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列读取接口
    /// @ 黄振东
    /// </summary>
    public interface IRabbitMessageQueueReader : IMessageQueueReader<RabbitMessageQueueInfo>
    {
        /// <summary>
        /// 根据交换机获取消息队列信息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <returns>消息队列信息</returns>
        RabbitMessageQueueInfo ReaderByExchange(string exchange);

        /// <summary>
        /// 根据队列获取消息队列信息
        /// </summary>
        /// <param name="queue">队列</param>
        /// <returns>消息队列信息</returns>
        RabbitMessageQueueInfo ReaderByQueue(string queue);
    }
}
