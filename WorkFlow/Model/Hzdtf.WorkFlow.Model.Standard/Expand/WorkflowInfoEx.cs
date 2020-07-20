using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 工作流信息
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowInfo
    {
        /// <summary>
        /// 处理列表
        /// </summary>
        [JsonProperty("handles")]
        [Display(Name = "处理列表", AutoGenerateField = false)]
        [MessagePack.Key("handles")]
        public IList<WorkflowHandleInfo> Handles
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [JsonProperty("workflowDefine")]
        [Display(Name = "工作流定义", AutoGenerateField = false)]
        [MessagePack.Key("workflowDefine")]
        public WorkflowDefineInfo WorkflowDefine
        {
            get;
            set;
        }
    }
}
