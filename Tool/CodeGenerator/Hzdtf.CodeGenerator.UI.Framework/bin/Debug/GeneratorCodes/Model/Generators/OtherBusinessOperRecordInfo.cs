using Hzdtf.Utility.Standard.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.AAG.Model.Standard
{
    /// <summary>
    /// 其他业务_操作记录信息
    /// @ 黄振东
    /// </summary>
    public partial class OtherBusinessOperRecordInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 内容_名称
        /// </summary>
		public const string Content_Name = "Content";

		/// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("content")]
        [MessagePack.Key("content")]
        [Required]
        [MaxLength(500)]

        [DisplayName("内容")]
        [Display(Name = "内容", Order = 1, AutoGenerateField = true)]
        public string Content
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 其他业务Id_名称
        /// </summary>
		public const string OtherBusinessId_Name = "OtherBusinessId";

		/// <summary>
        /// 其他业务Id
        /// </summary>
        [JsonProperty("otherBusinessId")]
        [MessagePack.Key("otherBusinessId")]
        [Required]

        [DisplayName("其他业务Id")]
        [Display(Name = "其他业务Id", Order = 9, AutoGenerateField = true)]
        public int OtherBusinessId
        {
            get;
            set;
        }
    }
}
