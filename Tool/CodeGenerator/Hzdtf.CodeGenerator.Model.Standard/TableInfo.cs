using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 表信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class TableInfo : BasicInfo
    {
        /// <summary>
        /// 列列表
        /// </summary>
        [JsonProperty("columns")]
        [MessagePack.Key("columns")]
        public IList<ColumnInfo> Columns
        {
            get;
            set;
        }
    }
}