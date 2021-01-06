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
    /// 角色信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class RoleInfo : CodeNameInfo<int>
    {
﻿        /// <summary>
        /// 备注_名称
        /// </summary>
		public const string Memo_Name = "Memo";

		/// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("memo")]
        [MaxLength(500)]

        [DisplayName("备注")]
        [Display(Name = "备注", Order = 99, AutoGenerateField = true)]
        [MessagePack.Key("memo")]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 系统隐藏_名称
        /// </summary>
		public const string SystemHide_Name = "SystemHide";

		/// <summary>
        /// 系统隐藏
        /// </summary>
        [JsonProperty("systemHide")]
        [Required]

        [DisplayName("系统隐藏")]
        [Display(Name = "系统隐藏", Order = 11, AutoGenerateField = false)]
        [MessagePack.Key("systemHide")]
        public bool SystemHide
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 系统内置_名称
        /// </summary>
		public const string SystemInlay_Name = "SystemInlay";

		/// <summary>
        /// 系统内置
        /// </summary>
        [JsonProperty("systemInlay")]
        [Required]

        [DisplayName("系统内置")]
        [Display(Name = "系统内置", Order = 12, AutoGenerateField = false)]
        [MessagePack.Key("systemInlay")]
        public bool SystemInlay
        {
            get;
            set;
        }
    }
}
