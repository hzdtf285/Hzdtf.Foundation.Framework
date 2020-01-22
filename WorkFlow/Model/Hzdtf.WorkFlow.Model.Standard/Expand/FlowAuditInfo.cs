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
    /// 流程审核信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FlowAuditInfo
    {
        /// <summary>
        /// 处理ID
        /// </summary>
        [JsonProperty("handlerId")]
        [DisplayName("处理ID")]
        [Display(Name = "处理ID")]
        [MessagePack.Key("handlerId")]
        public int HandleId
        {
            get;
            set;
        }

        /// <summary>
        /// 动作类型，默认为送件
        /// </summary>
        [JsonProperty("actionType")]
        [DisplayName("动作类型")]
        [Display(Name = "动作类型")]
        [MessagePack.Key("actionType")]
        public ActionType ActionType
        {
            get;
            set;
        } = ActionType.SEND;          

        /// <summary>
        /// 意见
        /// </summary>
        [JsonProperty("idea")]
        [MaxLength(200)]
        [DisplayName("意见")]
        [Display(Name = "意见")]
        [MessagePack.Key("idea")]
        public string Idea
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据JSON字符串
        /// </summary>
        [JsonProperty("formDataJsonString")]
        [MessagePack.Key("formDataJsonString")]
        public string FormDataJsonString
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流处理
        /// </summary>
        [JsonProperty("workflowHandle")]
        [MessagePack.Key("workflowHandle")]
        public WorkflowHandleInfo WorkflowHandle
        {
            get;
            set;
        }

        /// <summary>
        /// 转换为流程输入
        /// </summary>
        /// <returns>流程输入</returns>
        public FlowInInfo<FlowAuditInfo> ToFlowIn()
        {
            return new FlowInInfo<FlowAuditInfo>()
            {
                Flow = this
            };
        }
    }
}
