using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 具体表单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ConcreteFormInfo : PersonTimeInfo
    {
        /// <summary>
        /// 申请单号_名称
        /// </summary>
		public const string ApplyNo_Name = "ApplyNo";

        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        [MaxLength(20)]
        [DisplayName("申请单号")]
        [Display(Name = "申请单号", Order = 20, AutoGenerateField = true)]
        [MessagePack.Key("applyNo")]
        public string ApplyNo
        {
            get;
            set;
        }

        /// <summary>
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
        [Display(Name = "标题", Order = 22, AutoGenerateField = true)]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 流程状态_名称
        /// </summary>
        public const string FlowStatus_Name = "FlowStatus";

        /// <summary>
        /// 流程状态
        /// </summary>
        [JsonProperty("flowStatus")]
        [DisplayName("流程状态")]
        [Display(Name = "流程状态", Order = 21, AutoGenerateField = true)]
        [DisplayConvert(typeof(FlowStatusConvert))]
        [MessagePack.Key("flowStatus")]
        public FlowStatusEnum FlowStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流ID_名称
        /// </summary>
        public const string WorkflowId_Name = "WorkflowId";

        /// <summary>
        /// 工作流ID
        /// </summary>
        [JsonProperty("workflowId")]
        [DisplayName("工作流ID")]
        [Display(Name = "工作流ID", Order = 0, AutoGenerateField = false)]
        [MessagePack.Key("workflowId")]
        public int? WorkflowId
        {
            get;
            set;
        }
    }
}
