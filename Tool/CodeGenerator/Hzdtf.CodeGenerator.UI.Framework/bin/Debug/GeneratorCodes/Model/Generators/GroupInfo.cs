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
    /// GroupInfo信息
    /// @ 黄振东
    /// </summary>
    public partial class GroupInfo : PersonTimeInfo
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
        /// 当前流水号_名称
        /// </summary>
		public const string CurrSerialNo_Name = "CurrSerialNo";

		/// <summary>
        /// 当前流水号
        /// </summary>
        [JsonProperty("currSerialNo")]
        [MessagePack.Key("currSerialNo")]
        [Required]

        [DisplayName("当前流水号")]
        [Display(Name = "当前流水号", Order = 4, AutoGenerateField = true)]
        public long CurrSerialNo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否临时_名称
        /// </summary>
		public const string IsTemp_Name = "IsTemp";

		/// <summary>
        /// 是否临时
        /// </summary>
        [JsonProperty("isTemp")]
        [MessagePack.Key("isTemp")]
        [Required]

        [DisplayName("是否临时")]
        [Display(Name = "是否临时", Order = 6, AutoGenerateField = true)]
        public bool IsTemp
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 最后发送时间_名称
        /// </summary>
		public const string LastSendTime_Name = "LastSendTime";

		/// <summary>
        /// 最后发送时间
        /// </summary>
        [JsonProperty("lastSendTime")]
        [MessagePack.Key("lastSendTime")]
        [Required]

        [DisplayName("最后发送时间")]
        [Display(Name = "最后发送时间", Order = 7, AutoGenerateField = true)]
        public DateTime LastSendTime
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
        [MessagePack.Key("name")]
        [Required]
        [MaxLength(200)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 11, AutoGenerateField = true)]
        public string Name
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 父ID_名称
        /// </summary>
		public const string ParentId_Name = "ParentId";

		/// <summary>
        /// 父ID
        /// </summary>
        [JsonProperty("parentId")]
        [MessagePack.Key("parentId")]
        [MaxLength(36)]

        [DisplayName("父ID")]
        [Display(Name = "父ID", Order = 12, AutoGenerateField = true)]
        public string ParentId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 状态（0：正常，1：成员禁言；2：全员禁言）_名称
        /// </summary>
		public const string Status_Name = "Status";

		/// <summary>
        /// 状态（0：正常，1：成员禁言；2：全员禁言）
        /// </summary>
        [JsonProperty("status")]
        [MessagePack.Key("status")]
        [Required]

        [DisplayName("状态（0：正常，1：成员禁言；2：全员禁言）")]
        [Display(Name = "状态（0：正常，1：成员禁言；2：全员禁言）", Order = 13, AutoGenerateField = true)]
        public int Status
        {
            get;
            set;
        }
    }
}
