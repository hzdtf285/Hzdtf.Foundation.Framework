using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 流程信息
    /// @ 黄振东
    /// </summary>
    public partial class FlowInfo
    {
        /// <summary>
        /// 流程关卡数组
        /// </summary>
        [JsonProperty("functions")]
        [MessagePack.Key("functions")]
        public FlowCensorshipInfo[] FlowCensorships
        {
            get;
            set;
        }
    }
}
