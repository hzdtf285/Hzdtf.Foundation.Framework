using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 工作流处理信息
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowHandleInfo
    {
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
    }
}
