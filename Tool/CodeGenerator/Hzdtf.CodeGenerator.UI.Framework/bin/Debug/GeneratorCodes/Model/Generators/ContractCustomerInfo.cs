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
    /// 合约客户信息
    /// @ 黄振东
    /// </summary>
    public partial class ContractCustomerInfo : PersonTimeInfo
    {
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
        [Display(Name = "编码", Order = 1, AutoGenerateField = true)]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 客户资料Id_名称
        /// </summary>
		public const string CustomerId_Name = "CustomerId";

		/// <summary>
        /// 客户资料Id
        /// </summary>
        [JsonProperty("customerId")]
        [MessagePack.Key("customerId")]
        [Required]

        [DisplayName("客户资料Id")]
        [Display(Name = "客户资料Id", Order = 5, AutoGenerateField = true)]
        public int CustomerId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 开发票_名称
        /// </summary>
		public const string Invoice_Name = "Invoice";

		/// <summary>
        /// 开发票
        /// </summary>
        [JsonProperty("invoice")]
        [MessagePack.Key("invoice")]
        [Required]

        [DisplayName("开发票")]
        [Display(Name = "开发票", Order = 7, AutoGenerateField = true)]
        public bool Invoice
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否自动生成过账单_名称
        /// </summary>
		public const string IsAutoBuildBilled_Name = "IsAutoBuildBilled";

		/// <summary>
        /// 是否自动生成过账单
        /// </summary>
        [JsonProperty("isAutoBuildBilled")]
        [MessagePack.Key("isAutoBuildBilled")]
        [Required]

        [DisplayName("是否自动生成过账单")]
        [Display(Name = "是否自动生成过账单", Order = 8, AutoGenerateField = true)]
        public bool IsAutoBuildBilled
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否有合同_名称
        /// </summary>
		public const string IsExistsContract_Name = "IsExistsContract";

		/// <summary>
        /// 是否有合同
        /// </summary>
        [JsonProperty("isExistsContract")]
        [MessagePack.Key("isExistsContract")]
        [Required]

        [DisplayName("是否有合同")]
        [Display(Name = "是否有合同", Order = 9, AutoGenerateField = true)]
        public bool IsExistsContract
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 最后自动生成账单日期_名称
        /// </summary>
		public const string LastAutoBuildBillDate_Name = "LastAutoBuildBillDate";

		/// <summary>
        /// 最后自动生成账单日期
        /// </summary>
        [JsonProperty("lastAutoBuildBillDate")]
        [MessagePack.Key("lastAutoBuildBillDate")]

        [DisplayName("最后自动生成账单日期")]
        [Display(Name = "最后自动生成账单日期", Order = 10, AutoGenerateField = true)]
        public DateTime? LastAutoBuildBillDate
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
        [Display(Name = "备注", Order = 11, AutoGenerateField = true)]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 服务结束月份_名称
        /// </summary>
		public const string ServiceEndMonth_Name = "ServiceEndMonth";

		/// <summary>
        /// 服务结束月份
        /// </summary>
        [JsonProperty("serviceEndMonth")]
        [MessagePack.Key("serviceEndMonth")]
        [Required]

        [DisplayName("服务结束月份")]
        [Display(Name = "服务结束月份", Order = 15, AutoGenerateField = true)]
        public DateTime ServiceEndMonth
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 服务开始月份_名称
        /// </summary>
		public const string ServiceStartMonth_Name = "ServiceStartMonth";

		/// <summary>
        /// 服务开始月份
        /// </summary>
        [JsonProperty("serviceStartMonth")]
        [MessagePack.Key("serviceStartMonth")]
        [Required]

        [DisplayName("服务开始月份")]
        [Display(Name = "服务开始月份", Order = 16, AutoGenerateField = true)]
        public DateTime ServiceStartMonth
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算单价（单位：元）_名称
        /// </summary>
		public const string SettlePrice_Name = "SettlePrice";

		/// <summary>
        /// 结算单价（单位：元）
        /// </summary>
        [JsonProperty("settlePrice")]
        [MessagePack.Key("settlePrice")]
        [Required]

        [DisplayName("结算单价（单位：元）")]
        [Display(Name = "结算单价（单位：元）", Order = 17, AutoGenerateField = true)]
        public decimal SettlePrice
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算单位_名称
        /// </summary>
		public const string SettleUnit_Name = "SettleUnit";

		/// <summary>
        /// 结算单位
        /// </summary>
        [JsonProperty("settleUnit")]
        [MessagePack.Key("settleUnit")]
        [Required]

        [DisplayName("结算单位")]
        [Display(Name = "结算单位", Order = 18, AutoGenerateField = true)]
        public SettleUnitTypeEnum SettleUnit
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 签单日期_名称
        /// </summary>
		public const string SignBillDate_Name = "SignBillDate";

		/// <summary>
        /// 签单日期
        /// </summary>
        [JsonProperty("signBillDate")]
        [MessagePack.Key("signBillDate")]
        [Required]

        [DisplayName("签单日期")]
        [Display(Name = "签单日期", Order = 19, AutoGenerateField = true)]
        public DateTime SignBillDate
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
        [Display(Name = "状态", Order = 20, AutoGenerateField = true)]
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
        [Display(Name = "状态ID", Order = 21, AutoGenerateField = true)]
        public int StatusId
        {
            get;
            set;
        }
    }
}
