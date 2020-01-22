using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 页面信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class PageInfo
    {
        /// <summary>
        /// 功能列表
        /// </summary>
        [JsonProperty("functions")]
        [MessagePack.Key("functions")]
        public IList<CodeNameInfo> Functions
        {
            get;
            set;
        }
    }
}
