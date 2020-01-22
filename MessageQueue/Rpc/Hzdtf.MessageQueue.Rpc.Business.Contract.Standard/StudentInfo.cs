using MessagePack;
using Newtonsoft.Json;
using System;

namespace Hzdtf.MessageQueue.Rpc.Business.Contract.Standard
{
    /// <summary>
    /// 学生信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class StudentInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [Key("id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Key("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
