using Hzdtf.Utility.Standard.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hzdtd.Model.Standard
{
    /// <summary>
    /// 班级流水基数信息
    /// @ 黄振东
    /// </summary>
    public partial class ClassSerialBaseInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 基数_名称
        /// </summary>
		public const string BaseNum_Name = "BaseNum";

		/// <summary>
        /// 基数
        /// </summary>
        [JsonProperty("baseNum")]
        [MessagePack.Key("baseNum")]
        [Required]

        [DisplayName("基数")]
        [Display(Name = "基数", Order = 1, AutoGenerateField = true)]
        public long BaseNum
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 键_名称
        /// </summary>
		public const string Key_Name = "Key";

		/// <summary>
        /// 键
        /// </summary>
        [JsonProperty("key")]
        [MessagePack.Key("key")]
        [Required]
        [MaxLength(36)]

        [DisplayName("键")]
        [Display(Name = "键", Order = 3, AutoGenerateField = true)]
        public string Key
        {
            get;
            set;
        }
    }
}
