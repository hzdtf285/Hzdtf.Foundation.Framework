using Hzdtf.Utility.Standard.Connection;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Model.Standard.Connection
{
    /// <summary>
    /// Rabbit连接包装信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RabbitConnectionWrapInfo : ConnectionWrapInfo
    {
        /// <summary>
        /// 消息队列Json文件路径
        /// </summary>
        [MessagePack.Key("messageQueueJsonFile")]
        [JsonProperty("messageQueueJsonFile")]
        public string MessageQueueJsonFile
        {
            get;
            set;
        } = "Config/rabbitMessageQueue.json";
    }
}
