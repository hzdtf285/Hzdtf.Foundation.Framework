using Hzdtf.Utility.Standard.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.AAG.Model.Standard
{
    /// <summary>
    /// 收款统计明细信息
    /// @ 黄振东
    /// </summary>
    public partial class VPayeeTotalDetailInfo : PersonTimeInfo
    {
﻿        /// <summary>
        /// BusinessType_名称
        /// </summary>
		public const string BusinessType_Name = "BusinessType";

		/// <summary>
        /// BusinessType
        /// </summary>
        [JsonProperty("businessType")]
        [MessagePack.Key("businessType")]
        [Required]
        [MaxLength(4)]

        [DisplayName("BusinessType")]
        [Display(Name = "BusinessType", Order = 1, AutoGenerateField = true)]
        public string BusinessType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// CustomerShortName_名称
        /// </summary>
		public const string CustomerShortName_Name = "CustomerShortName";

		/// <summary>
        /// CustomerShortName
        /// </summary>
        [JsonProperty("customerShortName")]
        [MessagePack.Key("customerShortName")]
        [Required]
        [MaxLength(10)]

        [DisplayName("CustomerShortName")]
        [Display(Name = "CustomerShortName", Order = 2, AutoGenerateField = true)]
        public string CustomerShortName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// PayeeAmount_名称
        /// </summary>
		public const string PayeeAmount_Name = "PayeeAmount";

		/// <summary>
        /// PayeeAmount
        /// </summary>
        [JsonProperty("payeeAmount")]
        [MessagePack.Key("payeeAmount")]

        [DisplayName("PayeeAmount")]
        [Display(Name = "PayeeAmount", Order = 3, AutoGenerateField = true)]
        public decimal? PayeeAmount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// PayeeDate_名称
        /// </summary>
		public const string PayeeDate_Name = "PayeeDate";

		/// <summary>
        /// PayeeDate
        /// </summary>
        [JsonProperty("payeeDate")]
        [MessagePack.Key("payeeDate")]
        [Required]

        [DisplayName("PayeeDate")]
        [Display(Name = "PayeeDate", Order = 4, AutoGenerateField = true)]
        public DateTime PayeeDate
        {
            get;
            set;
        }

﻿        /// <summary>
        /// PayeeLeader_名称
        /// </summary>
		public const string PayeeLeader_Name = "PayeeLeader";

		/// <summary>
        /// PayeeLeader
        /// </summary>
        [JsonProperty("payeeLeader")]
        [MessagePack.Key("payeeLeader")]
        [Required]
        [MaxLength(20)]

        [DisplayName("PayeeLeader")]
        [Display(Name = "PayeeLeader", Order = 5, AutoGenerateField = true)]
        public string PayeeLeader
        {
            get;
            set;
        }

﻿        /// <summary>
        /// PayeeLeaderId_名称
        /// </summary>
		public const string PayeeLeaderId_Name = "PayeeLeaderId";

		/// <summary>
        /// PayeeLeaderId
        /// </summary>
        [JsonProperty("payeeLeaderId")]
        [MessagePack.Key("payeeLeaderId")]
        [Required]

        [DisplayName("PayeeLeaderId")]
        [Display(Name = "PayeeLeaderId", Order = 6, AutoGenerateField = true)]
        public int PayeeLeaderId
        {
            get;
            set;
        }
    }
}
