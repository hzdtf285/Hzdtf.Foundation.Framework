using Hzdtf.Utility.Standard.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FEG.Model.Standard
{
    /// <summary>
    /// 应用知识点信息
    /// @ 黄振东
    /// </summary>
    public partial class AppKnowledgePointInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 应用_Id_名称
        /// </summary>
		public const string AppId_Name = "AppId";

		/// <summary>
        /// 应用_Id
        /// </summary>
        [JsonProperty("appId")]
        [MessagePack.Key("appId")]
        [Required]

        [DisplayName("应用_Id")]
        [Display(Name = "应用_Id", Order = 2, AutoGenerateField = true)]
        public int AppId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 创建人ID_名称
        /// </summary>
		public const string CreatorId_Name = "CreatorId";

		/// <summary>
        /// 创建人ID
        /// </summary>
        [JsonProperty("creatorId")]
        [MessagePack.Key("creatorId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("创建人ID")]
        [Display(Name = "创建人ID", Order = 3, AutoGenerateField = true)]
        public string CreatorId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 创建人_名称
        /// </summary>
		public const string Creator_Name = "Creator";

		/// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty("creator")]
        [MessagePack.Key("creator")]
        [Required]
        [MaxLength(50)]

        [DisplayName("创建人")]
        [Display(Name = "创建人", Order = 4, AutoGenerateField = true)]
        public string Creator
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 删除_名称
        /// </summary>
		public const string Deleted_Name = "Deleted";

		/// <summary>
        /// 删除
        /// </summary>
        [JsonProperty("deleted")]
        [MessagePack.Key("deleted")]
        [Required]

        [DisplayName("删除")]
        [Display(Name = "删除", Order = 9, AutoGenerateField = true)]
        public bool Deleted
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 知识点ID_名称
        /// </summary>
		public const string KnowledgeId_Name = "KnowledgeId";

		/// <summary>
        /// 知识点ID
        /// </summary>
        [JsonProperty("knowledgeId")]
        [MessagePack.Key("knowledgeId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("知识点ID")]
        [Display(Name = "知识点ID", Order = 10, AutoGenerateField = true)]
        public string KnowledgeId
        {
            get;
            set;
        }
    }
}
