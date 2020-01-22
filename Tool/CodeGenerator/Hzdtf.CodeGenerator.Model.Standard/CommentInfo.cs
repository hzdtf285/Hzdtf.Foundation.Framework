using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 描述信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public sealed class CommentInfo
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
        [JsonProperty("desc")]
        [MessagePack.Key("desc")]
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 范围
        /// </summary>
        [JsonProperty("range")]
        [MessagePack.Key("range")]
        public double[] Range
        {
            get;
            set;
        }

        /// <summary>
        /// 最小长度
        /// </summary>
        [JsonProperty("minLength")]
        [MessagePack.Key("minLength")]
        public int? MinLength
        {
            get;
            set;
        }

        /// <summary>
        /// 枚举，如果不为空，则输出枚举类型
        /// </summary>
        [JsonProperty("enum")]
        [MessagePack.Key("enum")]
        public EnumInfo Enum
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    [MessagePackObject]
    public sealed class EnumInfo
    {
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [MessagePack.Key("desc")]
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 枚举子项集合
        /// </summary>
        [JsonProperty("items")]
        [MessagePack.Key("items")]
        public EnumItem[] Items
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 枚举子项
    /// </summary>
    [MessagePackObject]
    public sealed class EnumItem
    {
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty("value")]
        [MessagePack.Key("value")]
        public byte Value
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [MessagePack.Key("desc")]
        public string Desc
        {
            get;
            set;
        }
    }
}
