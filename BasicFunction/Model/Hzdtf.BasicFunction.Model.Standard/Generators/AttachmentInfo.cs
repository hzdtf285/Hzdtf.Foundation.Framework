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
    /// 附件信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class AttachmentInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 扩展名_名称
        /// </summary>
		public const string ExpandName_Name = "ExpandName";

		/// <summary>
        /// 扩展名
        /// </summary>
        [JsonProperty("expandName")]
        [MaxLength(10)]

        [DisplayName("扩展名")]
        [Display(Name = "扩展名", Order = 4, AutoGenerateField = true)]
        [MessagePack.Key("expandName")]
        public string ExpandName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文件地址_名称
        /// </summary>
		public const string FileAddress_Name = "FileAddress";

		/// <summary>
        /// 文件地址
        /// </summary>
        [JsonProperty("fileAddress")]
        [Required]
        [MaxLength(500)]

        [DisplayName("文件地址")]
        [Display(Name = "文件地址", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("fileAddress")]
        public string FileAddress
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文件名_名称
        /// </summary>
		public const string FileName_Name = "FileName";

		/// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("fileName")]
        [Required]
        [MaxLength(50)]

        [DisplayName("文件名")]
        [Display(Name = "文件名", Order = 6, AutoGenerateField = true)]
        [MessagePack.Key("fileName")]
        public string FileName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文件大小（KB）_名称
        /// </summary>
		public const string FileSize_Name = "FileSize";

		/// <summary>
        /// 文件大小（KB）
        /// </summary>
        [JsonProperty("fileSize")]
        [Required]

        [DisplayName("文件大小（KB）")]
        [Display(Name = "文件大小（KB）", Order = 7, AutoGenerateField = true)]
        [MessagePack.Key("fileSize")]
        public float FileSize
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 备注_名称
        /// </summary>
		public const string Memo_Name = "Memo";

		/// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("memo")]
        [MaxLength(500)]

        [DisplayName("备注")]
        [Display(Name = "备注", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("memo")]
        public string Memo
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属ID_名称
        /// </summary>
		public const string OwnerId_Name = "OwnerId";

		/// <summary>
        /// 归属ID
        /// </summary>
        [JsonProperty("ownerId")]

        [DisplayName("归属ID")]
        [Display(Name = "归属ID", Order = 13, AutoGenerateField = true)]
        [MessagePack.Key("ownerId")]
        public int OwnerId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属类型_名称
        /// </summary>
		public const string OwnerType_Name = "OwnerType";

		/// <summary>
        /// 归属类型
        /// </summary>
        [JsonProperty("ownerType")]
        [Required]

        [DisplayName("归属类型")]
        [Display(Name = "归属类型", Order = 14, AutoGenerateField = true)]
        [MessagePack.Key("ownerType")]
        public short OwnerType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 标题_名称
        /// </summary>
		public const string Title_Name = "Title";

		/// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [MaxLength(50)]

        [DisplayName("标题")]
        [Display(Name = "标题", Order = 15, AutoGenerateField = true)]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }
    }
}
