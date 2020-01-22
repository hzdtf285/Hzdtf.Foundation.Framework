using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 送件流程路线信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class SendFlowRouteInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 流程关卡Id_名称
        /// </summary>
		public const string FlowCensorshipId_Name = "FlowCensorshipId";

		/// <summary>
        /// 流程关卡Id
        /// </summary>
        [JsonProperty("flowCensorshipId")]
        [Required]

        [DisplayName("流程关卡Id")]
        [Display(Name = "流程关卡Id", Order = 4, AutoGenerateField = true)]
        [MessagePack.Key("flowCensorshipId")]
        public int FlowCensorshipId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 流转到流程关卡ID_名称
        /// </summary>
		public const string ToFlowCensorshipId_Name = "ToFlowCensorshipId";

		/// <summary>
        /// 流转到流程关卡ID
        /// </summary>
        [JsonProperty("toFlowCensorshipId")]
        [Required]

        [DisplayName("流转到流程关卡ID")]
        [Display(Name = "流转到流程关卡ID", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("toFlowCensorshipId")]
        public int ToFlowCensorshipId
        {
            get;
            set;
        }
    }
}
