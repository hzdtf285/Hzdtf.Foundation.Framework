using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 工作流信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class WorkflowInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 申请单号_名称
        /// </summary>
		public const string ApplyNo_Name = "ApplyNo";

		/// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        [Required]
        [MaxLength(20)]

        [DisplayName("申请单号")]
        [Display(Name = "申请单号", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("applyNo")]
        public string ApplyNo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 当前流程关卡ID（多个以逗号分隔）_名称
        /// </summary>
		public const string CurrConcreteCensorshipIds_Name = "CurrConcreteCensorshipIds";

		/// <summary>
        /// 当前流程关卡ID（多个以逗号分隔）
        /// </summary>
        [JsonProperty("currConcreteCensorshipIds")]
        [Required]
        [MaxLength(200)]

        [DisplayName("当前流程关卡ID（多个以逗号分隔）")]
        [Display(Name = "当前流程关卡ID（多个以逗号分隔）", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("currConcreteCensorshipIds")]
        public string CurrConcreteCensorshipIds
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 当前流程关卡（多个以逗号分隔）_名称
        /// </summary>
		public const string CurrConcreteCensorships_Name = "CurrConcreteCensorships";

		/// <summary>
        /// 当前流程关卡（多个以逗号分隔）
        /// </summary>
        [JsonProperty("currConcreteCensorships")]
        [Required]
        [MaxLength(2000)]

        [DisplayName("当前流程关卡（多个以逗号分隔）")]
        [Display(Name = "当前流程关卡（多个以逗号分隔）", Order = 6, AutoGenerateField = true)]
        [MessagePack.Key("currConcreteCensorships")]
        public string CurrConcreteCensorships
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 当前流程关卡ID（多个以逗号分隔）_名称
        /// </summary>
		public const string CurrFlowCensorshipIds_Name = "CurrFlowCensorshipIds";

		/// <summary>
        /// 当前流程关卡ID（多个以逗号分隔）
        /// </summary>
        [JsonProperty("currFlowCensorshipIds")]
        [Required]
        [MaxLength(200)]

        [DisplayName("当前流程关卡ID（多个以逗号分隔）")]
        [Display(Name = "当前流程关卡ID（多个以逗号分隔）", Order = 7, AutoGenerateField = true)]
        [MessagePack.Key("currFlowCensorshipIds")]
        public string CurrFlowCensorshipIds
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 当前处理人ID（多个以逗号分隔）_名称
        /// </summary>
		public const string CurrHandlerIds_Name = "CurrHandlerIds";

		/// <summary>
        /// 当前处理人ID（多个以逗号分隔）
        /// </summary>
        [JsonProperty("currHandlerIds")]
        [Required]
        [MaxLength(200)]

        [DisplayName("当前处理人ID（多个以逗号分隔）")]
        [Display(Name = "当前处理人ID（多个以逗号分隔）", Order = 8, AutoGenerateField = true)]
        [MessagePack.Key("currHandlerIds")]
        public string CurrHandlerIds
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 当前处理人（多个以逗号分隔）_名称
        /// </summary>
		public const string CurrHandlers_Name = "CurrHandlers";

		/// <summary>
        /// 当前处理人（多个以逗号分隔）
        /// </summary>
        [JsonProperty("currHandlers")]
        [Required]
        [MaxLength(2000)]

        [DisplayName("当前处理人（多个以逗号分隔）")]
        [Display(Name = "当前处理人（多个以逗号分隔）", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("currHandlers")]
        public string CurrHandlers
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
        [Required]

        [DisplayName("流程状态")]
        [Display(Name = "流程状态", Order = 10, AutoGenerateField = true)]
        [MessagePack.Key("flowStatus")]
        public FlowStatusEnum FlowStatus
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 标题_名称
        /// </summary>
		public const string Title_Name = "Title";

		/// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [Required]
        [MaxLength(50)]

        [DisplayName("标题")]
        [Display(Name = "标题", Order = 15, AutoGenerateField = true)]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 工作流定义Id_名称
        /// </summary>
		public const string WorkflowDefineId_Name = "WorkflowDefineId";

		/// <summary>
        /// 工作流定义Id
        /// </summary>
        [JsonProperty("workflowDefineId")]
        [Required]

        [DisplayName("工作流定义Id")]
        [Display(Name = "工作流定义Id", Order = 16, AutoGenerateField = true)]
        [MessagePack.Key("workflowDefineId")]
        public int WorkflowDefineId
        {
            get;
            set;
        }
    }
}
