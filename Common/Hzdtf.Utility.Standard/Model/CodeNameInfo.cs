using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 编码名称信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class CodeNameInfo : PersonTimeInfo
    {
        /// <summary>
        /// 编码_名称
        /// </summary>
        public const string Code_Name = "Code";

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MaxLength(20)]
        [DisplayName("编码")]
        [Display(Name = "编码", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 名称_名称
        /// </summary>
        public const string Name_Name = "Name";

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MaxLength(20)]
        [DisplayName("名称")]
        [Display(Name = "名称", Order = 2, AutoGenerateField = true)]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
