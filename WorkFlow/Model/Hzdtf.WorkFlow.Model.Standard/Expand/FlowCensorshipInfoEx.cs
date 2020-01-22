using Hzdtf.Utility.Standard.Model;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 流程关卡信息
    /// @ 黄振东
    /// </summary>
    public partial class FlowCensorshipInfo
    {
        /// <summary>
        /// 标准关卡数组
        /// </summary>
        [JsonProperty("standardCensorships")]
        [MessagePack.Key("standardCensorships")]
        public StandardCensorshipInfo[] StandardCensorships
        {
            get;
            set;
        }

        /// <summary>
        /// 送件路线数组
        /// </summary>
        [JsonProperty("sendFlowRoutes")]
        [MessagePack.Key("sendFlowRoutes")]
        public SendFlowRouteInfo[] SendFlowRoutes
        {
            get;
            set;
        }

        /// <summary>
        /// 退件路线数组
        /// </summary>
        [JsonProperty("returnFlowRoutes")]
        [MessagePack.Key("returnFlowRoutes")]
        public ReturnFlowRouteInfo[] ReturnFlowRoutes
        {
            get;
            set;
        }

        /// <summary>
        /// 具体关卡数组
        /// </summary>
        [JsonProperty("concreteCensorships")]
        [MessagePack.Key("concreteCensorships")]
        public ConcreteCensorshipInfo[] ConcreteCensorships
        {
            get;
            set;
        }
    }
}
