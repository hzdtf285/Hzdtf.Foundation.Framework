using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 关键字筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class KeywordFilterInfo : FilterInfo
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [JsonProperty("keyword")]
        [MessagePack.Key("keyword")]
        public string Keyword
        {
            get;
            set;
        }
    }
}
