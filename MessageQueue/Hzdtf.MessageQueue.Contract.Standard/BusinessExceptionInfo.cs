using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard
{
    /// <summary>
    /// 业务异常信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BusinessExceptionInfo
    {
        /// <summary>
        /// 服务名
        /// </summary>
        [JsonProperty("serviceName")]
        [Key("serviceName")]
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 异常消息
        /// </summary>
        [JsonProperty("exceptionMessage")]
        [Key("cxceptionMessage")]
        public string ExceptionMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 异常字符串
        /// </summary>
        [JsonProperty("exceptionString")]
        [Key("exceptionString")]
        public string ExceptionString
        {
            get;
            set;
        }

        /// <summary>
        /// 队列消息JSON字符串
        /// </summary>
        [JsonProperty("queueMessageJsonString")]
        [Key("queueMessageJsonString")]
        public string QueueMessageJsonString
        {
            get;
            set;
        }

        /// <summary>
        /// 交换机
        /// </summary>
        [JsonProperty("exchange")]
        [Key("exchange")]
        public string Exchange
        {
            get;
            set;
        }

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

        /// <summary>
        /// 时间
        /// </summary>
        [JsonProperty("time")]
        [Key("time")]
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [Key("desc")]
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器名
        /// </summary>
        [JsonProperty("serverMachineName")]
        [Key("serverMachineName")]
        public string ServerMachineName
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器IP
        /// </summary>
        [JsonProperty("serverIP")]
        [Key("serverIP")]
        public string ServerIP
        {
            get;
            set;
        }        

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return JsonUtil.SerializeIgnoreNull(this);
        }
    }
}
