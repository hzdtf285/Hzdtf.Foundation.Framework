using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 工作流定义信息
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowDefineInfo
    {
        /// <summary>
        /// 流程信息
        /// </summary>
        [JsonProperty("flow")]
        [MessagePack.Key("flow")]
        public FlowInfo Flow
        {
            get;
            set;
        }

        /// <summary>
        /// 表单信息
        /// </summary>
        [JsonProperty("form")]
        [MessagePack.Key("form")]
        public FormInfo Form
        {
            get;
            set;
        }
    }
}
