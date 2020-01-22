using Hzdtf.Utility.Standard.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hzdtd.Model.Standard
{
    /// <summary>
    /// GroupUserInfo信息
    /// @ 黄振东
    /// </summary>
    public partial class GroupUserInfo : PersonTimeInfo
    {
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
        [Display(Name = "创建人", Order = 2, AutoGenerateField = true)]
        public string Creator
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
        /// 群组ID_名称
        /// </summary>
		public const string GroupId_Name = "GroupId";

		/// <summary>
        /// 群组ID
        /// </summary>
        [JsonProperty("groupId")]
        [MessagePack.Key("groupId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("群组ID")]
        [Display(Name = "群组ID", Order = 4, AutoGenerateField = true)]
        public string GroupId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户ID_名称
        /// </summary>
		public const string UserId_Name = "UserId";

		/// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("userId")]
        [MessagePack.Key("userId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("用户ID")]
        [Display(Name = "用户ID", Order = 9, AutoGenerateField = true)]
        public string UserId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户名_名称
        /// </summary>
		public const string UserName_Name = "UserName";

		/// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("userName")]
        [MessagePack.Key("userName")]
        [MaxLength(50)]

        [DisplayName("用户名")]
        [Display(Name = "用户名", Order = 10, AutoGenerateField = true)]
        public string UserName
        {
            get;
            set;
        }
    }
}
