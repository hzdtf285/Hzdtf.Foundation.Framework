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
    /// 群流水键信息
    /// @ 黄振东
    /// </summary>
    public partial class GroupSerialKeyInfo : PersonTimeInfo
    {
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
        [Display(Name = "键", Order = 2, AutoGenerateField = true)]
        public string Key
        {
            get;
            set;
        }
    }
}
