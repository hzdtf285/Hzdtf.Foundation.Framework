using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Filter
{
    /// <summary>
    /// 工作流筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class WorkflowFilterInfo : KeywordFilterInfo
    {
        /// <summary>
        /// 处理人ID
        /// </summary>
        [JsonProperty("handlerId")]
        [MessagePack.Key("handlerId")]
        public int HandlerId
        {
            get;
            set;
        }
    }
}
