using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Conversion;
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
    /// 用户信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class UserInfo : BasicUserInfo
    {
﻿        /// <summary>
        /// 邮箱_名称
        /// </summary>
		public const string Mail_Name = "Mail";

		/// <summary>
        /// 邮箱
        /// </summary>
        [JsonProperty("mail")]
        [MaxLength(50)]

        [DisplayName("邮箱")]
        [Display(Name = "邮箱", Order = 8, AutoGenerateField = true)]
        [MessagePack.Key("mail")]
        public string Mail
        {
            get;
            set;
        }

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
        /// 手机_名称
        /// </summary>
		public const string Mobile_Name = "Mobile";

		/// <summary>
        /// 手机
        /// </summary>
        [JsonProperty("mobile")]
        [MaxLength(11)]

        [DisplayName("手机")]
        [Display(Name = "手机", Order = 10, AutoGenerateField = true)]
        [MessagePack.Key("mobile")]
        public string Mobile
        {
            get;
            set;
        }

﻿        /// <summary>
        /// QQ_名称
        /// </summary>
		public const string QQ_Name = "QQ";

		/// <summary>
        /// QQ
        /// </summary>
        [JsonProperty("qq")]
        [MaxLength(20)]

        [DisplayName("QQ")]
        [Display(Name = "QQ", Order = 16, AutoGenerateField = true)]
        [MessagePack.Key("qq")]
        public string QQ
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 性别_名称
        /// </summary>
		public const string Sex_Name = "Sex";

		/// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        [Required]
        [DisplayConvert(typeof(SexTextConvert))]

        [DisplayName("性别")]
        [Display(Name = "性别", Order = 17, AutoGenerateField = true)]
        [MessagePack.Key("sex")]
        public bool Sex
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
        [Display(Name = "系统隐藏", Order = 18, AutoGenerateField = false)]
        [MessagePack.Key("systemHide")]
        public bool SystemHide
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 微信_名称
        /// </summary>
		public const string Wechat_Name = "Wechat";

		/// <summary>
        /// 微信
        /// </summary>
        [JsonProperty("wechat")]
        [MaxLength(20)]

        [DisplayName("微信")]
        [Display(Name = "微信", Order = 20, AutoGenerateField = true)]
        [MessagePack.Key("wechat")]
        public string Wechat
        {
            get;
            set;
        }
    }
}
