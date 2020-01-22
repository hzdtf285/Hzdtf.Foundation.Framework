using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.MessageQueue
{
    /// <summary>
    /// 消息队列信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class MessageQueueInfo
    {
        /// <summary>
        /// 队列
        /// </summary>
        [JsonProperty("queue")]
        [Key("queue")]
        public string Queue
        {
            get;
            set;
        }
    }
}
