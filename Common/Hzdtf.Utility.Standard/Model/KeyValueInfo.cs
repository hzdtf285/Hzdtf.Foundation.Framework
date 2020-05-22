using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 键值信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    [MessagePackObject]
    public class KeyValueInfo<KeyT, ValueT>
    {
        /// <summary>
        /// 键
        /// </summary>
        [JsonProperty("key")]
        [MessagePack.Key("key")]
        public KeyT Key
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty("value")]
        [MessagePack.Key("value")]
        public ValueT Value
        {
            get;
            set;
        }
    }
}
