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
    /// 菜单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class MenuInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 图标_名称
        /// </summary>
		public const string Icon_Name = "Icon";

		/// <summary>
        /// 图标
        /// </summary>
        [JsonProperty("icon")]
        [MaxLength(20)]

        [DisplayName("图标")]
        [Display(Name = "图标", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("icon")]
        public string Icon
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 链接_名称
        /// </summary>
		public const string Link_Name = "Link";

		/// <summary>
        /// 链接
        /// </summary>
        [JsonProperty("link")]
        [MaxLength(200)]

        [DisplayName("链接")]
        [Display(Name = "链接", Order = 7, AutoGenerateField = true)]
        [MessagePack.Key("link")]
        public string Link
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
        [Required]
        [MaxLength(20)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 11, AutoGenerateField = true)]
        [MessagePack.Key("name")]
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
        [Required]

        [DisplayName("父ID")]
        [Display(Name = "父ID", Order = 12, AutoGenerateField = true)]
        [MessagePack.Key("parentId")]
        public int ParentId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 排序_名称
        /// </summary>
		public const string Sort_Name = "Sort";

		/// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("sort")]
        [Required]

        [DisplayName("排序")]
        [Display(Name = "排序", Order = 13, AutoGenerateField = true)]
        [MessagePack.Key("sort")]
        public int Sort
        {
            get;
            set;
        }
    }
}
