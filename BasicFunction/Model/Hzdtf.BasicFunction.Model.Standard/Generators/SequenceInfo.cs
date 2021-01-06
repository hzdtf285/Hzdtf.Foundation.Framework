using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 序列信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class SequenceInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 增量_名称
        /// </summary>
		public const string Increment_Name = "Increment";

		/// <summary>
        /// 增量
        /// </summary>
        [JsonProperty("increment")]
        [Required]

        [DisplayName("增量")]
        [Display(Name = "增量", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("increment")]
        public int Increment
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 序列类型_名称
        /// </summary>
		public const string SeqType_Name = "SeqType";

		/// <summary>
        /// 序列类型
        /// </summary>
        [JsonProperty("seqType")]
        [Required]
        [MaxLength(2)]

        [DisplayName("序列类型")]
        [Display(Name = "序列类型", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("seqType")]
        public string SeqType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 更新日期_名称
        /// </summary>
		public const string UpdateDate_Name = "UpdateDate";

		/// <summary>
        /// 更新日期
        /// </summary>
        [JsonProperty("updateDate")]
        [Required]

        [DisplayName("更新日期")]
        [Display(Name = "更新日期", Order = 10, AutoGenerateField = true)]
        [MessagePack.Key("updateDate")]
        public DateTime UpdateDate
        {
            get;
            set;
        }
    }
}
