using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 简单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class SimpleInfo : ICloneable
    {
        /// <summary>
        /// ID_名称
        /// </summary>
        public const string Id_Name = "Id";

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns>拷贝后的对象</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => JsonUtil.SerializeIgnoreNull(this);
    }
}
