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
    /// 班级信息
    /// @ 黄振东
    /// </summary>
    public partial class ClassesInfo : PersonTimeInfo
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
    }
}
