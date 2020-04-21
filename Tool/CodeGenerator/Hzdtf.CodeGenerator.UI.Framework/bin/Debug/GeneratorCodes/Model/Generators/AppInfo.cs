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
    /// 应用信息
    /// @ 黄振东
    /// </summary>
    public partial class AppInfo : PersonTimeInfo
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
        [Display(Name = "描述", Order = 3, AutoGenerateField = true)]
        public string Desc
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 图标URL_名称
        /// </summary>
		public const string IconAddressId_Name = "IconAddressId";

		/// <summary>
        /// 图标URL
        /// </summary>
        [JsonProperty("iconAddressId")]
        [MessagePack.Key("iconAddressId")]
        [Required]
        [MaxLength(36)]

        [DisplayName("图标URL")]
        [Display(Name = "图标URL", Order = 4, AutoGenerateField = true)]
        public string IconAddressId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 下载次数基数_名称
        /// </summary>
		public const string DownBaseCount_Name = "DownBaseCount";

		/// <summary>
        /// 下载次数基数
        /// </summary>
        [JsonProperty("downBaseCount")]
        [MessagePack.Key("downBaseCount")]
        [Required]

        [DisplayName("下载次数基数")]
        [Display(Name = "下载次数基数", Order = 5, AutoGenerateField = true)]
        public int DownBaseCount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 实际下载次数_名称
        /// </summary>
		public const string RealDownCount_Name = "RealDownCount";

		/// <summary>
        /// 实际下载次数
        /// </summary>
        [JsonProperty("realDownCount")]
        [MessagePack.Key("realDownCount")]
        [Required]

        [DisplayName("实际下载次数")]
        [Display(Name = "实际下载次数", Order = 6, AutoGenerateField = true)]
        public int RealDownCount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 下载次数_名称
        /// </summary>
		public const string DownCount_Name = "DownCount";

		/// <summary>
        /// 下载次数
        /// </summary>
        [JsonProperty("downCount")]
        [MessagePack.Key("downCount")]
        [Required]

        [DisplayName("下载次数")]
        [Display(Name = "下载次数", Order = 7, AutoGenerateField = true)]
        public int DownCount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 公司_名称
        /// </summary>
		public const string Company_Name = "Company";

		/// <summary>
        /// 公司
        /// </summary>
        [JsonProperty("company")]
        [MessagePack.Key("company")]
        [MaxLength(200)]

        [DisplayName("公司")]
        [Display(Name = "公司", Order = 8, AutoGenerateField = true)]
        public string Company
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 0：未审核，1：审核中，2：已上架，3：已下架_名称
        /// </summary>
		public const string Status_Name = "Status";

		/// <summary>
        /// 0：未审核，1：审核中，2：已上架，3：已下架
        /// </summary>
        [JsonProperty("status")]
        [MessagePack.Key("status")]
        [Required]

        [DisplayName("0：未审核，1：审核中，2：已上架，3：已下架")]
        [Display(Name = "0：未审核，1：审核中，2：已上架，3：已下架", Order = 9, AutoGenerateField = true)]
        public int Status
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否推荐_名称
        /// </summary>
		public const string IsRecommend_Name = "IsRecommend";

		/// <summary>
        /// 是否推荐
        /// </summary>
        [JsonProperty("isRecommend")]
        [MessagePack.Key("isRecommend")]
        [Required]

        [DisplayName("是否推荐")]
        [Display(Name = "是否推荐", Order = 10, AutoGenerateField = true)]
        public bool IsRecommend
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 推荐时间_名称
        /// </summary>
		public const string RecommendTime_Name = "RecommendTime";

		/// <summary>
        /// 推荐时间
        /// </summary>
        [JsonProperty("recommendTime")]
        [MessagePack.Key("recommendTime")]

        [DisplayName("推荐时间")]
        [Display(Name = "推荐时间", Order = 11, AutoGenerateField = true)]
        public DateTime? RecommendTime
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 评分_名称
        /// </summary>
		public const string Score_Name = "Score";

		/// <summary>
        /// 评分
        /// </summary>
        [JsonProperty("score")]
        [MessagePack.Key("score")]
        [Required]

        [DisplayName("评分")]
        [Display(Name = "评分", Order = 12, AutoGenerateField = true)]
        public float Score
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 评论数_名称
        /// </summary>
		public const string CommentNum_Name = "CommentNum";

		/// <summary>
        /// 评论数
        /// </summary>
        [JsonProperty("commentNum")]
        [MessagePack.Key("commentNum")]
        [Required]

        [DisplayName("评论数")]
        [Display(Name = "评论数", Order = 13, AutoGenerateField = true)]
        public int CommentNum
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 好评率_名称
        /// </summary>
		public const string FeedbackRate_Name = "FeedbackRate";

		/// <summary>
        /// 好评率
        /// </summary>
        [JsonProperty("feedbackRate")]
        [MessagePack.Key("feedbackRate")]
        [Required]

        [DisplayName("好评率")]
        [Display(Name = "好评率", Order = 14, AutoGenerateField = true)]
        public float FeedbackRate
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 安装次数基数_名称
        /// </summary>
		public const string InstallBaseCount_Name = "InstallBaseCount";

		/// <summary>
        /// 安装次数基数
        /// </summary>
        [JsonProperty("installBaseCount")]
        [MessagePack.Key("installBaseCount")]
        [Required]

        [DisplayName("安装次数基数")]
        [Display(Name = "安装次数基数", Order = 15, AutoGenerateField = true)]
        public int InstallBaseCount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 实际安装次数_名称
        /// </summary>
		public const string RealInstallCount_Name = "RealInstallCount";

		/// <summary>
        /// 实际安装次数
        /// </summary>
        [JsonProperty("realInstallCount")]
        [MessagePack.Key("realInstallCount")]
        [Required]

        [DisplayName("实际安装次数")]
        [Display(Name = "实际安装次数", Order = 16, AutoGenerateField = true)]
        public int RealInstallCount
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 安装次数_名称
        /// </summary>
		public const string InstallCount_Name = "InstallCount";

		/// <summary>
        /// 安装次数
        /// </summary>
        [JsonProperty("installCount")]
        [MessagePack.Key("installCount")]
        [Required]

        [DisplayName("安装次数")]
        [Display(Name = "安装次数", Order = 17, AutoGenerateField = true)]
        public int InstallCount
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
        [Display(Name = "创建人ID", Order = 18, AutoGenerateField = true)]
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
        [Display(Name = "创建人", Order = 19, AutoGenerateField = true)]
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
        [Display(Name = "删除", Order = 24, AutoGenerateField = true)]
        public bool Deleted
        {
            get;
            set;
        }
    }
}
