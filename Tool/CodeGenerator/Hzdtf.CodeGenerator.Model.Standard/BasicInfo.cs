using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 基本信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BasicInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        [MessagePack.Key("description")]
        public string Description
        {
            get;
            set;
        }
    }
}
