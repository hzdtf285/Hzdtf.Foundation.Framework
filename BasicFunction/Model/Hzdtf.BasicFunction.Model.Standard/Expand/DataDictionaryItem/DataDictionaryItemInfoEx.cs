using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 数据字典子项
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemInfo
    {
        /// <summary>
        /// 扩展列表
        /// </summary>
        [JsonProperty("expands")]
        [MessagePack.Key("expands")]
        public IList<DataDictionaryItemExpandInfo> Expands
        {
            get;
            set;
        }
    }
}
