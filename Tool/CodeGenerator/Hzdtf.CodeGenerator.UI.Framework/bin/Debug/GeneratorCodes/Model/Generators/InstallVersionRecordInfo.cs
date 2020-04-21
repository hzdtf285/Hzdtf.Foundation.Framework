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
    /// 安装版本记录信息
    /// @ 黄振东
    /// </summary>
    public partial class InstallVersionRecordInfo : PersonTimeInfo
    {
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
        [Display(Name = "创建人ID", Order = 2, AutoGenerateField = true)]
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
        [Display(Name = "创建人", Order = 3, AutoGenerateField = true)]
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
        [Display(Name = "删除", Order = 8, AutoGenerateField = true)]
        public bool Deleted
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 应用版本Id_名称
        /// </summary>
		public const string AppVersionId_Name = "AppVersionId";

		/// <summary>
        /// 应用版本Id
        /// </summary>
        [JsonProperty("appVersionId")]
        [MessagePack.Key("appVersionId")]
        [Required]

        [DisplayName("应用版本Id")]
        [Display(Name = "应用版本Id", Order = 9, AutoGenerateField = true)]
        public int AppVersionId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 操作人ID_名称
        /// </summary>
		public const string OperPersonId_Name = "OperPersonId";

		/// <summary>
        /// 操作人ID
        /// </summary>
        [JsonProperty("operPersonId")]
        [MessagePack.Key("operPersonId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("操作人ID")]
        [Display(Name = "操作人ID", Order = 10, AutoGenerateField = true)]
        public string OperPersonId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 操作人_名称
        /// </summary>
		public const string OperPerson_Name = "OperPerson";

		/// <summary>
        /// 操作人
        /// </summary>
        [JsonProperty("operPerson")]
        [MessagePack.Key("operPerson")]
        [Required]
        [MaxLength(50)]

        [DisplayName("操作人")]
        [Display(Name = "操作人", Order = 11, AutoGenerateField = true)]
        public string OperPerson
        {
            get;
            set;
        }

﻿        /// <summary>
        /// SourceIp_名称
        /// </summary>
		public const string SourceIp_Name = "SourceIp";

		/// <summary>
        /// SourceIp
        /// </summary>
        [JsonProperty("sourceIp")]
        [MessagePack.Key("sourceIp")]
        [Required]
        [MaxLength(15)]

        [DisplayName("SourceIp")]
        [Display(Name = "SourceIp", Order = 12, AutoGenerateField = true)]
        public string SourceIp
        {
            get;
            set;
        }
    }
}
