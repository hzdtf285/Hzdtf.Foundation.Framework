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
    /// 其他业务信息
    /// @ 黄振东
    /// </summary>
    public partial class OtherBusinessInfo : PersonTimeInfo
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
        [Required]
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
        [Required]

        [DisplayName("会计负责人ID")]
        [Display(Name = "会计负责人ID", Order = 2, AutoGenerateField = true)]
        public int AccountLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 业务详细要求_名称
        /// </summary>
		public const string BusineDescClaim_Name = "BusineDescClaim";

		/// <summary>
        /// 业务详细要求
        /// </summary>
        [JsonProperty("busineDescClaim")]
        [MessagePack.Key("busineDescClaim")]
        [MaxLength(200)]

        [DisplayName("业务详细要求")]
        [Display(Name = "业务详细要求", Order = 3, AutoGenerateField = true)]
        public string BusineDescClaim
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
        [Required]
        [MaxLength(20)]

        [DisplayName("业务负责人")]
        [Display(Name = "业务负责人", Order = 4, AutoGenerateField = true)]
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
        [Required]

        [DisplayName("业务负责人ID")]
        [Display(Name = "业务负责人ID", Order = 5, AutoGenerateField = true)]
        public int BusinessLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 业务类型_名称
        /// </summary>
		public const string BusinessType_Name = "BusinessType";

		/// <summary>
        /// 业务类型
        /// </summary>
        [JsonProperty("businessType")]
        [MessagePack.Key("businessType")]
        [Required]

        [DisplayName("业务类型")]
        [Display(Name = "业务类型", Order = 6, AutoGenerateField = true)]
        public BusinessTypeEnum BusinessType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 具体要求_名称
        /// </summary>
		public const string Claim_Name = "Claim";

		/// <summary>
        /// 具体要求
        /// </summary>
        [JsonProperty("claim")]
        [MessagePack.Key("claim")]
        [Required]
        [MaxLength(200)]

        [DisplayName("具体要求")]
        [Display(Name = "具体要求", Order = 7, AutoGenerateField = true)]
        public string Claim
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 合同金额（单位：元）_名称
        /// </summary>
		public const string ContractAmount_Name = "ContractAmount";

		/// <summary>
        /// 合同金额（单位：元）
        /// </summary>
        [JsonProperty("contractAmount")]
        [MessagePack.Key("contractAmount")]
        [Required]

        [DisplayName("合同金额（单位：元）")]
        [Display(Name = "合同金额（单位：元）", Order = 8, AutoGenerateField = true)]
        public decimal ContractAmount
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
        [Display(Name = "客户资料Id", Order = 12, AutoGenerateField = true)]
        public int CustomerId
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
        [Display(Name = "减免金额（单位：元）", Order = 13, AutoGenerateField = true)]
        public decimal DiscountAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否完结_名称
        /// </summary>
		public const string IsEnd_Name = "IsEnd";

		/// <summary>
        /// 是否完结
        /// </summary>
        [JsonProperty("isEnd")]
        [MessagePack.Key("isEnd")]
        [Required]

        [DisplayName("是否完结")]
        [Display(Name = "是否完结", Order = 15, AutoGenerateField = true)]
        public bool IsEnd
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
        [Display(Name = "是否有合同", Order = 16, AutoGenerateField = true)]
        public bool IsExistsContract
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 外勤负责人_名称
        /// </summary>
		public const string LegworkLeader_Name = "LegworkLeader";

		/// <summary>
        /// 外勤负责人
        /// </summary>
        [JsonProperty("legworkLeader")]
        [MessagePack.Key("legworkLeader")]
        [Required]
        [MaxLength(20)]

        [DisplayName("外勤负责人")]
        [Display(Name = "外勤负责人", Order = 17, AutoGenerateField = true)]
        public string LegworkLeader
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 外勤负责人ID_名称
        /// </summary>
		public const string LegworkLeaderId_Name = "LegworkLeaderId";

		/// <summary>
        /// 外勤负责人ID
        /// </summary>
        [JsonProperty("legworkLeaderId")]
        [MessagePack.Key("legworkLeaderId")]
        [Required]

        [DisplayName("外勤负责人ID")]
        [Display(Name = "外勤负责人ID", Order = 18, AutoGenerateField = true)]
        public int LegworkLeaderId
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
        [Display(Name = "备注", Order = 19, AutoGenerateField = true)]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 已收款金额（单位：元）_名称
        /// </summary>
		public const string MoneyPeceiptAmount_Name = "MoneyPeceiptAmount";

		/// <summary>
        /// 已收款金额（单位：元）
        /// </summary>
        [JsonProperty("moneyPeceiptAmount")]
        [MessagePack.Key("moneyPeceiptAmount")]
        [Required]

        [DisplayName("已收款金额（单位：元）")]
        [Display(Name = "已收款金额（单位：元）", Order = 23, AutoGenerateField = true)]
        public decimal MoneyPeceiptAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 收款描述_名称
        /// </summary>
		public const string MoneyReceiptDesc_Name = "MoneyReceiptDesc";

		/// <summary>
        /// 收款描述
        /// </summary>
        [JsonProperty("moneyReceiptDesc")]
        [MessagePack.Key("moneyReceiptDesc")]
        [MaxLength(200)]

        [DisplayName("收款描述")]
        [Display(Name = "收款描述", Order = 24, AutoGenerateField = true)]
        public string MoneyReceiptDesc
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否收齐资料_名称
        /// </summary>
		public const string PackData_Name = "PackData";

		/// <summary>
        /// 是否收齐资料
        /// </summary>
        [JsonProperty("packData")]
        [MessagePack.Key("packData")]
        [Required]

        [DisplayName("是否收齐资料")]
        [Display(Name = "是否收齐资料", Order = 25, AutoGenerateField = true)]
        public bool PackData
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 进度_名称
        /// </summary>
		public const string Progress_Name = "Progress";

		/// <summary>
        /// 进度
        /// </summary>
        [JsonProperty("progress")]
        [MessagePack.Key("progress")]
        [Required]
        [MaxLength(200)]

        [DisplayName("进度")]
        [Display(Name = "进度", Order = 26, AutoGenerateField = true)]
        public string Progress
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
        [Display(Name = "签单日期", Order = 27, AutoGenerateField = true)]
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
        [Display(Name = "状态", Order = 28, AutoGenerateField = true)]
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
        [Display(Name = "状态ID", Order = 29, AutoGenerateField = true)]
        public int StatusId
        {
            get;
            set;
        }
    }
}
