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
    /// 业务异常记录信息
    /// @ 黄振东
    /// </summary>
    public partial class BusinessExceptionRecordInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// 描述_名称
        /// </summary>
		public const string Desc_Name = "Desc";

		/// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [MessagePack.Key("desc")]
        [MaxLength(2000)]

        [DisplayName("描述")]
        [Display(Name = "描述", Order = 2, AutoGenerateField = true)]
        public string Desc
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 异常消息_名称
        /// </summary>
		public const string ExceptionMessage_Name = "ExceptionMessage";

		/// <summary>
        /// 异常消息
        /// </summary>
        [JsonProperty("exceptionMessage")]
        [MessagePack.Key("exceptionMessage")]
        [MaxLength(2000)]

        [DisplayName("异常消息")]
        [Display(Name = "异常消息", Order = 3, AutoGenerateField = true)]
        public string ExceptionMessage
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 异常字符串_名称
        /// </summary>
		public const string ExceptionString_Name = "ExceptionString";

		/// <summary>
        /// 异常字符串
        /// </summary>
        [JsonProperty("exceptionString")]
        [MessagePack.Key("exceptionString")]
        [MaxLength(65535)]

        [DisplayName("异常字符串")]
        [Display(Name = "异常字符串", Order = 4, AutoGenerateField = true)]
        public string ExceptionString
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 交换机_名称
        /// </summary>
		public const string Exchange_Name = "Exchange";

		/// <summary>
        /// 交换机
        /// </summary>
        [JsonProperty("exchange")]
        [MessagePack.Key("exchange")]
        [MaxLength(100)]

        [DisplayName("交换机")]
        [Display(Name = "交换机", Order = 5, AutoGenerateField = true)]
        public string Exchange
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 队列_名称
        /// </summary>
		public const string Queue_Name = "Queue";

		/// <summary>
        /// 队列
        /// </summary>
        [JsonProperty("queue")]
        [MessagePack.Key("queue")]
        [MaxLength(100)]

        [DisplayName("队列")]
        [Display(Name = "队列", Order = 7, AutoGenerateField = true)]
        public string Queue
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 队列消息_名称
        /// </summary>
		public const string QueueMessage_Name = "QueueMessage";

		/// <summary>
        /// 队列消息
        /// </summary>
        [JsonProperty("queueMessage")]
        [MessagePack.Key("queueMessage")]
        [MaxLength(65535)]

        [DisplayName("队列消息")]
        [Display(Name = "队列消息", Order = 8, AutoGenerateField = true)]
        public string QueueMessage
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 服务器IP_名称
        /// </summary>
		public const string ServerIp_Name = "ServerIp";

		/// <summary>
        /// 服务器IP
        /// </summary>
        [JsonProperty("serverIp")]
        [MessagePack.Key("serverIp")]
        [MaxLength(50)]

        [DisplayName("服务器IP")]
        [Display(Name = "服务器IP", Order = 9, AutoGenerateField = true)]
        public string ServerIp
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 服务器名_名称
        /// </summary>
		public const string ServerMachineName_Name = "ServerMachineName";

		/// <summary>
        /// 服务器名
        /// </summary>
        [JsonProperty("serverMachineName")]
        [MessagePack.Key("serverMachineName")]
        [MaxLength(100)]

        [DisplayName("服务器名")]
        [Display(Name = "服务器名", Order = 10, AutoGenerateField = true)]
        public string ServerMachineName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 服务名_名称
        /// </summary>
		public const string ServiceName_Name = "ServiceName";

		/// <summary>
        /// 服务名
        /// </summary>
        [JsonProperty("serviceName")]
        [MessagePack.Key("serviceName")]
        [MaxLength(50)]

        [DisplayName("服务名")]
        [Display(Name = "服务名", Order = 11, AutoGenerateField = true)]
        public string ServiceName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 时间_名称
        /// </summary>
		public const string Time_Name = "Time";

		/// <summary>
        /// 时间
        /// </summary>
        [JsonProperty("time")]
        [MessagePack.Key("time")]
        [Required]

        [DisplayName("时间")]
        [Display(Name = "时间", Order = 12, AutoGenerateField = true)]
        public DateTime Time
        {
            get;
            set;
        }
    }
}
