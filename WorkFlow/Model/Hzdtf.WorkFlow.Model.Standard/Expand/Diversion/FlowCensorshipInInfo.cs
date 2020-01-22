using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Diversion
{
    /// <summary>
    /// 流程关卡输入信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FlowCensorshipInInfo
    {
        /// <summary>
        /// 当前工作流处理，如果为null，则为申请者刚申请
        /// </summary>
        [JsonProperty("currWorkflowHandle")]
        [MessagePack.Key("currWorkflowHandle")]
        public WorkflowHandleInfo CurrWorkflowHandle
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流定义，不能为空
        /// </summary>
        [JsonProperty("workflowDefine")]
        [MessagePack.Key("workflowDefine")]
        public WorkflowDefineInfo WorkflowDefine
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流
        /// </summary>
        [JsonProperty("workflow")]
        [MessagePack.Key("workflow")]
        public WorkflowInfo Workflow
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 表单ID
        /// </summary>
        [JsonProperty("formId")]
        [MessagePack.Key("formId")]
        public int FormId
        {
            get;
            set;
        }

        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        [MessagePack.Key("applyNo")]
        public string ApplyNo
        {
            get;
            set;
        }

        /// <summary>
        /// 意见
        /// </summary>
        [JsonProperty("idea")]
        [MessagePack.Key("idea")]
        public string Idea
        {
            get;
            set;
        }

        /// <summary>
        /// 动作类型，默认为送件
        /// </summary>
        [JsonProperty("actionType")]
        [MessagePack.Key("actionType")]
        public ActionType ActionType
        {
            get;
            set;
        } = ActionType.SEND;
    }
}
