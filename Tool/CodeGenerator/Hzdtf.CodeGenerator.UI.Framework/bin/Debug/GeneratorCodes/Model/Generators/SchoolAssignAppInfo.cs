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
    /// 学校指定应用信息
    /// @ 黄振东
    /// </summary>
    public partial class SchoolAssignAppInfo : PersonTimeInfo
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
        /// 学校ID_名称
        /// </summary>
		public const string SchoolId_Name = "SchoolId";

		/// <summary>
        /// 学校ID
        /// </summary>
        [JsonProperty("schoolId")]
        [MessagePack.Key("schoolId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("学校ID")]
        [Display(Name = "学校ID", Order = 10, AutoGenerateField = true)]
        public string SchoolId
        {
            get;
            set;
        }
    }
}
