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
    /// 流程关卡信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class FlowCensorshipInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 流程Id_名称
        /// </summary>
		public const string FlowId_Name = "FlowId";

		/// <summary>
        /// 流程Id
        /// </summary>
        [JsonProperty("flowId")]
        [Required]

        [DisplayName("流程Id")]
        [Display(Name = "流程Id", Order = 4, AutoGenerateField = true)]
        [MessagePack.Key("flowId")]
        public int FlowId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属关卡ID_名称
        /// </summary>
		public const string OwnerCensorshipId_Name = "OwnerCensorshipId";

		/// <summary>
        /// 归属关卡ID
        /// </summary>
        [JsonProperty("ownerCensorshipId")]
        [Required]

        [DisplayName("归属关卡ID")]
        [Display(Name = "归属关卡ID", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("ownerCensorshipId")]
        public int OwnerCensorshipId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属关卡类型_名称
        /// </summary>
		public const string OwnerCensorshipType_Name = "OwnerCensorshipType";

		/// <summary>
        /// 归属关卡类型
        /// </summary>
        [JsonProperty("ownerCensorshipType")]
        [Required]

        [DisplayName("归属关卡类型")]
        [Display(Name = "归属关卡类型", Order = 10, AutoGenerateField = true)]
        [MessagePack.Key("ownerCensorshipType")]
        public CensorshipTypeEnum OwnerCensorshipType
        {
            get;
            set;
        }
    }
}
