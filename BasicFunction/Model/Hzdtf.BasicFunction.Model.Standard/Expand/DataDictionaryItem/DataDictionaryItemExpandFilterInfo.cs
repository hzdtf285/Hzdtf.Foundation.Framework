using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.DataDictionaryItem
{
    /// <summary>
    /// 数据子项扩展筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class DataDictionaryItemExpandFilterInfo : FilterInfo
    {
        /// <summary>
        /// 数据字典子项ID
        /// </summary>
        [JsonProperty("dataDictionaryItemId")]
        [MessagePack.Key("dataDictionaryItemId")]
        public int DataDictionaryItemId
        {
            get;
            set;
        }
    }
}
