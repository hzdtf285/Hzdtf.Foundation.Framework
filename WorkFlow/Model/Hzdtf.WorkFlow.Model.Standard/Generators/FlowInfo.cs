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
    /// 流程信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class FlowInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 名称_名称
        /// </summary>
		public const string Name_Name = "Name";

		/// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Required]
        [MaxLength(20)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 8, AutoGenerateField = true)]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
