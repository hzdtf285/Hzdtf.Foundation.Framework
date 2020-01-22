using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Filter
{
    /// <summary>
    /// 待处理筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class WaitHandleFilterInfo : WorkflowFilterInfo
    {
        /// <summary>
        /// 处理类型
        /// </summary>
        [JsonProperty("handleType")]
        [MessagePack.Key("handleType")]
        public byte? HandleType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已读
        /// </summary>
        [JsonProperty("isReaded")]
        [MessagePack.Key("isReaded")]
        public bool? IsReaded
        {
            get;
            set;
        }
    }
}
