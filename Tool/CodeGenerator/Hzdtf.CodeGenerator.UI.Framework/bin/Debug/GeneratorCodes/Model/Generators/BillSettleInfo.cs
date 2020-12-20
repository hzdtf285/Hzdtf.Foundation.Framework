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
    /// 账单结算信息
    /// @ 黄振东
    /// </summary>
    public partial class BillSettleInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 会计负责人_名称
        /// </summary>
		public const string AccountLeader_Name = "AccountLeader";

		/// <summary>
        /// 会计负责人
        /// </summary>
        [JsonProperty("accountLeader")]
        [MessagePack.Key("accountLeader")]
        [MaxLength(20)]

        [DisplayName("会计负责人")]
        [Display(Name = "会计负责人", Order = 1, AutoGenerateField = true)]
        public string AccountLeader
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 会计负责人ID_名称
        /// </summary>
		public const string AccountLeaderId_Name = "AccountLeaderId";

		/// <summary>
        /// 会计负责人ID
        /// </summary>
        [JsonProperty("accountLeaderId")]
        [MessagePack.Key("accountLeaderId")]

        [DisplayName("会计负责人ID")]
        [Display(Name = "会计负责人ID", Order = 2, AutoGenerateField = true)]
        public int? AccountLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 自动生成_名称
        /// </summary>
		public const string AutoBuilder_Name = "AutoBuilder";

		/// <summary>
        /// 自动生成
        /// </summary>
        [JsonProperty("autoBuilder")]
        [MessagePack.Key("autoBuilder")]
        [Required]

        [DisplayName("自动生成")]
        [Display(Name = "自动生成", Order = 3, AutoGenerateField = true)]
        public bool AutoBuilder
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 账单月份_名称
        /// </summary>
		public const string BillMonth_Name = "BillMonth";

		/// <summary>
        /// 账单月份
        /// </summary>
        [JsonProperty("billMonth")]
        [MessagePack.Key("billMonth")]
        [Required]

        [DisplayName("账单月份")]
        [Display(Name = "账单月份", Order = 4, AutoGenerateField = true)]
        public DateTime BillMonth
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 业务负责人_名称
        /// </summary>
		public const string BusinessLeader_Name = "BusinessLeader";

		/// <summary>
        /// 业务负责人
        /// </summary>
        [JsonProperty("businessLeader")]
        [MessagePack.Key("businessLeader")]
        [MaxLength(20)]

        [DisplayName("业务负责人")]
        [Display(Name = "业务负责人", Order = 5, AutoGenerateField = true)]
        public string BusinessLeader
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 业务负责人ID_名称
        /// </summary>
		public const string BusinessLeaderId_Name = "BusinessLeaderId";

		/// <summary>
        /// 业务负责人ID
        /// </summary>
        [JsonProperty("businessLeaderId")]
        [MessagePack.Key("businessLeaderId")]

        [DisplayName("业务负责人ID")]
        [Display(Name = "业务负责人ID", Order = 6, AutoGenerateField = true)]
        public int? BusinessLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 合约客户Id_名称
        /// </summary>
		public const string ContractCustomerId_Name = "ContractCustomerId";

		/// <summary>
        /// 合约客户Id
        /// </summary>
        [JsonProperty("contractCustomerId")]
        [MessagePack.Key("contractCustomerId")]
        [Required]

        [DisplayName("合约客户Id")]
        [Display(Name = "合约客户Id", Order = 7, AutoGenerateField = true)]
        public int ContractCustomerId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 费用金额（单位：元）_名称
        /// </summary>
		public const string CostAmount_Name = "CostAmount";

		/// <summary>
        /// 费用金额（单位：元）
        /// </summary>
        [JsonProperty("costAmount")]
        [MessagePack.Key("costAmount")]
        [Required]

        [DisplayName("费用金额（单位：元）")]
        [Display(Name = "费用金额（单位：元）", Order = 8, AutoGenerateField = true)]
        public decimal CostAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 减免金额（单位：元）_名称
        /// </summary>
		public const string DiscountAmount_Name = "DiscountAmount";

		/// <summary>
        /// 减免金额（单位：元）
        /// </summary>
        [JsonProperty("discountAmount")]
        [MessagePack.Key("discountAmount")]
        [Required]

        [DisplayName("减免金额（单位：元）")]
        [Display(Name = "减免金额（单位：元）", Order = 12, AutoGenerateField = true)]
        public decimal DiscountAmount
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
        [Display(Name = "开发票", Order = 14, AutoGenerateField = true)]
        public bool Invoice
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
        [Display(Name = "备注", Order = 15, AutoGenerateField = true)]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 收款金额（单位：元）_名称
        /// </summary>
		public const string PayeeAmount_Name = "PayeeAmount";

		/// <summary>
        /// 收款金额（单位：元）
        /// </summary>
        [JsonProperty("payeeAmount")]
        [MessagePack.Key("payeeAmount")]
        [Required]

        [DisplayName("收款金额（单位：元）")]
        [Display(Name = "收款金额（单位：元）", Order = 19, AutoGenerateField = true)]
        public decimal PayeeAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 收款负责人_名称
        /// </summary>
		public const string PayeeLeader_Name = "PayeeLeader";

		/// <summary>
        /// 收款负责人
        /// </summary>
        [JsonProperty("payeeLeader")]
        [MessagePack.Key("payeeLeader")]
        [MaxLength(20)]

        [DisplayName("收款负责人")]
        [Display(Name = "收款负责人", Order = 20, AutoGenerateField = true)]
        public string PayeeLeader
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 收款负责人ID_名称
        /// </summary>
		public const string PayeeLeaderId_Name = "PayeeLeaderId";

		/// <summary>
        /// 收款负责人ID
        /// </summary>
        [JsonProperty("payeeLeaderId")]
        [MessagePack.Key("payeeLeaderId")]

        [DisplayName("收款负责人ID")]
        [Display(Name = "收款负责人ID", Order = 21, AutoGenerateField = true)]
        public int? PayeeLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 对账详情_名称
        /// </summary>
		public const string ReconciliationDesc_Name = "ReconciliationDesc";

		/// <summary>
        /// 对账详情
        /// </summary>
        [JsonProperty("reconciliationDesc")]
        [MessagePack.Key("reconciliationDesc")]
        [MaxLength(200)]

        [DisplayName("对账详情")]
        [Display(Name = "对账详情", Order = 22, AutoGenerateField = true)]
        public string ReconciliationDesc
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算日期_名称
        /// </summary>
		public const string SettleDate_Name = "SettleDate";

		/// <summary>
        /// 结算日期
        /// </summary>
        [JsonProperty("settleDate")]
        [MessagePack.Key("settleDate")]

        [DisplayName("结算日期")]
        [Display(Name = "结算日期", Order = 23, AutoGenerateField = true)]
        public DateTime? SettleDate
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算状态（0：未结算；1：已结算）_名称
        /// </summary>
		public const string SettleStatus_Name = "SettleStatus";

		/// <summary>
        /// 结算状态（0：未结算；1：已结算）
        /// </summary>
        [JsonProperty("settleStatus")]
        [MessagePack.Key("settleStatus")]
        [Required]

        [DisplayName("结算状态（0：未结算；1：已结算）")]
        [Display(Name = "结算状态（0：未结算；1：已结算）", Order = 24, AutoGenerateField = true)]
        public bool SettleStatus
        {
            get;
            set;
        }
    }
}
