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
    /// 客户资料信息
    /// @ 黄振东
    /// </summary>
    public partial class CustomerDataInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 地址_名称
        /// </summary>
		public const string Address_Name = "Address";

		/// <summary>
        /// 地址
        /// </summary>
        [JsonProperty("address")]
        [MessagePack.Key("address")]
        [MaxLength(200)]

        [DisplayName("地址")]
        [Display(Name = "地址", Order = 1, AutoGenerateField = true)]
        public string Address
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 2, AutoGenerateField = true)]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 公司电话_名称
        /// </summary>
		public const string CompanyTel_Name = "CompanyTel";

		/// <summary>
        /// 公司电话
        /// </summary>
        [JsonProperty("companyTel")]
        [MessagePack.Key("companyTel")]
        [MaxLength(20)]

        [DisplayName("公司电话")]
        [Display(Name = "公司电话", Order = 3, AutoGenerateField = true)]
        public string CompanyTel
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 联系人_名称
        /// </summary>
		public const string Contact_Name = "Contact";

		/// <summary>
        /// 联系人
        /// </summary>
        [JsonProperty("contact")]
        [MessagePack.Key("contact")]
        [MaxLength(20)]

        [DisplayName("联系人")]
        [Display(Name = "联系人", Order = 4, AutoGenerateField = true)]
        public string Contact
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 联系人电话_名称
        /// </summary>
		public const string ContactTel_Name = "ContactTel";

		/// <summary>
        /// 联系人电话
        /// </summary>
        [JsonProperty("contactTel")]
        [MessagePack.Key("contactTel")]
        [MaxLength(20)]

        [DisplayName("联系人电话")]
        [Display(Name = "联系人电话", Order = 5, AutoGenerateField = true)]
        public string ContactTel
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 客户名称_名称
        /// </summary>
		public const string CustomerName_Name = "CustomerName";

		/// <summary>
        /// 客户名称
        /// </summary>
        [JsonProperty("customerName")]
        [MessagePack.Key("customerName")]
        [Required]
        [MaxLength(100)]

        [DisplayName("客户名称")]
        [Display(Name = "客户名称", Order = 9, AutoGenerateField = true)]
        public string CustomerName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 客户简称_名称
        /// </summary>
		public const string CustomerShortName_Name = "CustomerShortName";

		/// <summary>
        /// 客户简称
        /// </summary>
        [JsonProperty("customerShortName")]
        [MessagePack.Key("customerShortName")]
        [Required]
        [MaxLength(10)]

        [DisplayName("客户简称")]
        [Display(Name = "客户简称", Order = 10, AutoGenerateField = true)]
        public string CustomerShortName
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
        [MessagePack.Key("memo")]
        [MaxLength(500)]

        [DisplayName("备注")]
        [Display(Name = "备注", Order = 12, AutoGenerateField = true)]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 状态_名称
        /// </summary>
		public const string Status_Name = "Status";

		/// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        [MessagePack.Key("status")]
        [Required]
        [MaxLength(20)]

        [DisplayName("状态")]
        [Display(Name = "状态", Order = 16, AutoGenerateField = true)]
        public string Status
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 状态ID_名称
        /// </summary>
		public const string StatusId_Name = "StatusId";

		/// <summary>
        /// 状态ID
        /// </summary>
        [JsonProperty("statusId")]
        [MessagePack.Key("statusId")]
        [Required]

        [DisplayName("状态ID")]
        [Display(Name = "状态ID", Order = 17, AutoGenerateField = true)]
        public int StatusId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 统一信用代码_名称
        /// </summary>
		public const string Uscc_Name = "Uscc";

		/// <summary>
        /// 统一信用代码
        /// </summary>
        [JsonProperty("uscc")]
        [MessagePack.Key("uscc")]
        [MaxLength(100)]

        [DisplayName("统一信用代码")]
        [Display(Name = "统一信用代码", Order = 18, AutoGenerateField = true)]
        public string Uscc
        {
            get;
            set;
        }
    }
}
