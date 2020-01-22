using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Filter
{
    /// <summary>
    /// 审核流程筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class AuditFlowFilterInfo : WorkflowFilterInfo
    {
        /// <summary>
        /// 流程状态
        /// </summary>
        [JsonProperty("flowStatus")]
        [MessagePack.Key("flowStatus")]
        public byte? FlowStatus
        {
            get;
            set;
        }
    }
}
