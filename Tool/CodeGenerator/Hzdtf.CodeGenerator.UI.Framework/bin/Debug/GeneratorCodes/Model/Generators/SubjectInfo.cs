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
    /// 学科信息
    /// @ 黄振东
    /// </summary>
    public partial class SubjectInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 名称_名称
        /// </summary>
		public const string Name_Name = "Name";

		/// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        [Required]
        [MaxLength(50)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 2, AutoGenerateField = true)]
        public string Name
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 关联其他ID集合，多个以,分隔，如果只有一个，前后也要求加,_名称
        /// </summary>
		public const string AssociateOtherIds_Name = "AssociateOtherIds";

		/// <summary>
        /// 关联其他ID集合，多个以,分隔，如果只有一个，前后也要求加,
        /// </summary>
        [JsonProperty("associateOtherIds")]
        [MessagePack.Key("associateOtherIds")]
        [MaxLength(2000)]

        [DisplayName("关联其他ID集合，多个以,分隔，如果只有一个，前后也要求加,")]
        [Display(Name = "关联其他ID集合，多个以,分隔，如果只有一个，前后也要求加,", Order = 3, AutoGenerateField = true)]
        public string AssociateOtherIds
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
        [Display(Name = "创建人ID", Order = 4, AutoGenerateField = true)]
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
        [Display(Name = "创建人", Order = 5, AutoGenerateField = true)]
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
        [Display(Name = "删除", Order = 10, AutoGenerateField = true)]
        public bool Deleted
        {
            get;
            set;
        }
    }
}
