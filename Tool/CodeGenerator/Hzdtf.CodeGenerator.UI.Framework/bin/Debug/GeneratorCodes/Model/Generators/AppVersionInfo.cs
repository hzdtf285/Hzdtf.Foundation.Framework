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
    /// 应用版本信息
    /// @ 黄振东
    /// </summary>
    public partial class AppVersionInfo : PersonTimeInfo
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
        /// 下载地址ID_名称
        /// </summary>
		public const string DownAddressId_Name = "DownAddressId";

		/// <summary>
        /// 下载地址ID
        /// </summary>
        [JsonProperty("downAddressId")]
        [MessagePack.Key("downAddressId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("下载地址ID")]
        [Display(Name = "下载地址ID", Order = 10, AutoGenerateField = true)]
        public string DownAddressId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 版本_名称
        /// </summary>
		public const string Version_Name = "Version";

		/// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("version")]
        [MessagePack.Key("version")]
        [Required]
        [MaxLength(20)]

        [DisplayName("版本")]
        [Display(Name = "版本", Order = 11, AutoGenerateField = true)]
        public string Version
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 版本号_名称
        /// </summary>
		public const string VersionNum_Name = "VersionNum";

		/// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty("versionNum")]
        [MessagePack.Key("versionNum")]
        [Required]

        [DisplayName("版本号")]
        [Display(Name = "版本号", Order = 12, AutoGenerateField = true)]
        public int VersionNum
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 更新内容_名称
        /// </summary>
		public const string UpdateContent_Name = "UpdateContent";

		/// <summary>
        /// 更新内容
        /// </summary>
        [JsonProperty("updateContent")]
        [MessagePack.Key("updateContent")]
        [Required]
        [MaxLength(500)]

        [DisplayName("更新内容")]
        [Display(Name = "更新内容", Order = 13, AutoGenerateField = true)]
        public string UpdateContent
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 大小（B）_名称
        /// </summary>
		public const string Size_Name = "Size";

		/// <summary>
        /// 大小（B）
        /// </summary>
        [JsonProperty("size")]
        [MessagePack.Key("size")]
        [Required]

        [DisplayName("大小（B）")]
        [Display(Name = "大小（B）", Order = 14, AutoGenerateField = true)]
        public int Size
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 上传时间_名称
        /// </summary>
		public const string UploadTime_Name = "UploadTime";

		/// <summary>
        /// 上传时间
        /// </summary>
        [JsonProperty("uploadTime")]
        [MessagePack.Key("uploadTime")]
        [Required]

        [DisplayName("上传时间")]
        [Display(Name = "上传时间", Order = 15, AutoGenerateField = true)]
        public DateTime UploadTime
        {
            get;
            set;
        }
    }
}
