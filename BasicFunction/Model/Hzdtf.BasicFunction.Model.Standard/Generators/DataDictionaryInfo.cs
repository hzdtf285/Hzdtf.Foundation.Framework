using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 数据字典信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class DataDictionaryInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文本_名称
        /// </summary>
		public const string Text_Name = "Text";

		/// <summary>
        /// 文本
        /// </summary>
        [JsonProperty("text")]
        [Required]
        [MaxLength(20)]

        [DisplayName("文本")]
        [Display(Name = "文本", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("text")]
        public string Text
        {
            get;
            set;
        }
    }
}
