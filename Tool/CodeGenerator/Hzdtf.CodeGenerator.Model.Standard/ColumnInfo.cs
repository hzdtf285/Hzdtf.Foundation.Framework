using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 列信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ColumnInfo : BasicInfo
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        [JsonProperty("dataType")]
        [MessagePack.Key("dataType")]
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        [JsonProperty("columnType")]
        [MessagePack.Key("columnType")]
        public string ColumnType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许为null
        /// </summary>
        [JsonProperty("isNull")]
        [MessagePack.Key("isNull")]
        public bool IsNull
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        [JsonProperty("length")]
        [MessagePack.Key("length")]
        public int? Length
        {
            get;
            set;
        }
    }
}
