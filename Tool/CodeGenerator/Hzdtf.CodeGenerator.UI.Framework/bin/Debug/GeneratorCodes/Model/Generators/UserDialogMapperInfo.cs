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
    /// 用户对话映射信息
    /// @ 黄振东
    /// </summary>
    public partial class UserDialogMapperInfo : PersonTimeInfo
    {
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
        [Display(Name = "当前流水号", Order = 1, AutoGenerateField = true)]
        public long CurrSerialNo
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
        [Display(Name = "最后发送时间", Order = 3, AutoGenerateField = true)]
        public DateTime LastSendTime
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户ID1_名称
        /// </summary>
		public const string UserId1_Name = "UserId1";

		/// <summary>
        /// 用户ID1
        /// </summary>
        [JsonProperty("userId1")]
        [MessagePack.Key("userId1")]
        [Required]
        [MaxLength(36)]

        [DisplayName("用户ID1")]
        [Display(Name = "用户ID1", Order = 4, AutoGenerateField = true)]
        public string UserId1
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户ID2_名称
        /// </summary>
		public const string UserId2_Name = "UserId2";

		/// <summary>
        /// 用户ID2
        /// </summary>
        [JsonProperty("userId2")]
        [MessagePack.Key("userId2")]
        [Required]
        [MaxLength(36)]

        [DisplayName("用户ID2")]
        [Display(Name = "用户ID2", Order = 5, AutoGenerateField = true)]
        public string UserId2
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户名1_名称
        /// </summary>
		public const string UserName1_Name = "UserName1";

		/// <summary>
        /// 用户名1
        /// </summary>
        [JsonProperty("userName1")]
        [MessagePack.Key("userName1")]
        [Required]
        [MaxLength(50)]

        [DisplayName("用户名1")]
        [Display(Name = "用户名1", Order = 6, AutoGenerateField = true)]
        public string UserName1
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户名2_名称
        /// </summary>
		public const string UserName2_Name = "UserName2";

		/// <summary>
        /// 用户名2
        /// </summary>
        [JsonProperty("userName2")]
        [MessagePack.Key("userName2")]
        [Required]
        [MaxLength(50)]

        [DisplayName("用户名2")]
        [Display(Name = "用户名2", Order = 7, AutoGenerateField = true)]
        public string UserName2
        {
            get;
            set;
        }
    }
}
