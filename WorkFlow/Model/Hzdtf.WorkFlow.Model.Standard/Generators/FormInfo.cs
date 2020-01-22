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
    /// 表单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class FormInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 表单URL_名称
        /// </summary>
		public const string FormUrl_Name = "FormUrl";

		/// <summary>
        /// 表单URL
        /// </summary>
        [JsonProperty("formUrl")]
        [Required]
        [MaxLength(200)]

        [DisplayName("表单URL")]
        [Display(Name = "表单URL", Order = 4, AutoGenerateField = true)]
        [MessagePack.Key("formUrl")]
        public string FormUrl
        {
            get;
            set;
        }

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
        [Display(Name = "名称", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
