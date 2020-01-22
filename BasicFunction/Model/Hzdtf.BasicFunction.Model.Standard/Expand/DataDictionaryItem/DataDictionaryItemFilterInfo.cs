using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.DataDictionaryItem
{
    /// <summary>
    /// 数据子项筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class DataDictionaryItemFilterInfo : FilterInfo
    {
        /// <summary>
        /// 数据字典ID
        /// </summary>
        [JsonProperty("dataDictionaryId")]
        [MessagePack.Key("dataDictionaryId")]
        public int DataDictionaryId
        {
            get;
            set;
        }
    }
}
