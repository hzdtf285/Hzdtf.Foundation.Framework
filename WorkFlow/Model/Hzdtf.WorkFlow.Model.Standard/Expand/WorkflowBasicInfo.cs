using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 工作流基本信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class WorkflowBasicInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [MessagePack.Key("id")]
        public int Id
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
    }
}
