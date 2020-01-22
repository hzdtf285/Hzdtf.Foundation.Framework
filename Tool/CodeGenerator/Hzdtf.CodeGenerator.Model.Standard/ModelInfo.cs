using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 模型信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ModelInfo : BasicInfo
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        [JsonProperty("propertyType")]
        [MessagePack.Key("propertyType")]
        public string PropertyType
        {
            get;
            set;
        }

        /// <summary>
        /// 特性列表
        /// </summary>
        [JsonProperty("attrs")]
        [MessagePack.Key("attrs")]
        public IList<string> Attrs
        {
            get;
            set;
        }
    }
}
