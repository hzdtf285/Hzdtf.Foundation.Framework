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
    /// 合约客户_收款明细信息
    /// @ 黄振东
    /// </summary>
    public partial class ContractCustomerPayeeDetailInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 申请单号_名称
        /// </summary>
		public const string ApplyNo_Name = "ApplyNo";

		/// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        [MessagePack.Key("applyNo")]
        [MaxLength(20)]

        [DisplayName("申请单号")]
        [Display(Name = "申请单号", Order = 1, AutoGenerateField = true)]
        public string ApplyNo
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
        [Display(Name = "合约客户Id", Order = 2, AutoGenerateField = true)]
        public int ContractCustomerId
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
        [Display(Name = "减免金额（单位：元）", Order = 6, AutoGenerateField = true)]
        public decimal DiscountAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 流程状态_名称
        /// </summary>
		public const string FlowStatus_Name = "FlowStatus";

		/// <summary>
        /// 流程状态
        /// </summary>
        [JsonProperty("flowStatus")]
        [MessagePack.Key("flowStatus")]
        [Required]

        [DisplayName("流程状态")]
        [Display(Name = "流程状态", Order = 7, AutoGenerateField = true)]
        public FlowStatusEnum FlowStatus
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
        [Display(Name = "备注", Order = 9, AutoGenerateField = true)]
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
        [Display(Name = "收款金额（单位：元）", Order = 13, AutoGenerateField = true)]
        public decimal PayeeAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 收款日期_名称
        /// </summary>
		public const string PayeeDate_Name = "PayeeDate";

		/// <summary>
        /// 收款日期
        /// </summary>
        [JsonProperty("payeeDate")]
        [MessagePack.Key("payeeDate")]
        [Required]

        [DisplayName("收款日期")]
        [Display(Name = "收款日期", Order = 14, AutoGenerateField = true)]
        public DateTime PayeeDate
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
        [Required]
        [MaxLength(20)]

        [DisplayName("收款负责人")]
        [Display(Name = "收款负责人", Order = 15, AutoGenerateField = true)]
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
        [Required]

        [DisplayName("收款负责人ID")]
        [Display(Name = "收款负责人ID", Order = 16, AutoGenerateField = true)]
        public int PayeeLeaderId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算结束月份_名称
        /// </summary>
		public const string SettleEndMonth_Name = "SettleEndMonth";

		/// <summary>
        /// 结算结束月份
        /// </summary>
        [JsonProperty("settleEndMonth")]
        [MessagePack.Key("settleEndMonth")]
        [Required]

        [DisplayName("结算结束月份")]
        [Display(Name = "结算结束月份", Order = 17, AutoGenerateField = true)]
        public DateTime SettleEndMonth
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 结算开始月份_名称
        /// </summary>
		public const string SettleStartMonth_Name = "SettleStartMonth";

		/// <summary>
        /// 结算开始月份
        /// </summary>
        [JsonProperty("settleStartMonth")]
        [MessagePack.Key("settleStartMonth")]
        [Required]

        [DisplayName("结算开始月份")]
        [Display(Name = "结算开始月份", Order = 18, AutoGenerateField = true)]
        public DateTime SettleStartMonth
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 工作流ID_名称
        /// </summary>
		public const string WorkflowId_Name = "WorkflowId";

		/// <summary>
        /// 工作流ID
        /// </summary>
        [JsonProperty("workflowId")]
        [MessagePack.Key("workflowId")]

        [DisplayName("工作流ID")]
        [Display(Name = "工作流ID", Order = 19, AutoGenerateField = true)]
        public int? WorkflowId
        {
            get;
            set;
        }
    }
}
