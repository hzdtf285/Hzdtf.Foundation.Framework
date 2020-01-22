using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 编码名称筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class CodeNameFilterInfo : FilterInfo
    {
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 模糊名称
        /// </summary>
        [JsonProperty("blurName")]
        [Key("blurName")]
        public string BlurName
        {
            get;
            set;
        }
    }
}
