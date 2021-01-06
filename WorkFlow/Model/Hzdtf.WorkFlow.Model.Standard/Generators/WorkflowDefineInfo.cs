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
    /// 工作流定义信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class WorkflowDefineInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 启用_名称
        /// </summary>
		public const string Enabled_Name = "Enabled";

		/// <summary>
        /// 启用
        /// </summary>
        [JsonProperty("enabled")]
        [Required]

        [DisplayName("启用")]
        [Display(Name = "启用", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("enabled")]
        public bool Enabled
        {
            get;
            set;
        }

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
        [Display(Name = "流程Id", Order = 6, AutoGenerateField = true)]
        [MessagePack.Key("flowId")]
        public int FlowId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 表单Id_名称
        /// </summary>
		public const string FormId_Name = "FormId";

		/// <summary>
        /// 表单Id
        /// </summary>
        [JsonProperty("formId")]
        [Required]

        [DisplayName("表单Id")]
        [Display(Name = "表单Id", Order = 7, AutoGenerateField = true)]
        [MessagePack.Key("formId")]
        public int FormId
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
        [Display(Name = "名称", Order = 12, AutoGenerateField = true)]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }
    }
}
