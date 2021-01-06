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
    /// 用户菜单功能信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class UserMenuFunctionInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 菜单功能Id_名称
        /// </summary>
		public const string MenuFunctionId_Name = "MenuFunctionId";

		/// <summary>
        /// 菜单功能Id
        /// </summary>
        [JsonProperty("menuFunctionId")]
        [Required]

        [DisplayName("菜单功能Id")]
        [Display(Name = "菜单功能Id", Order = 5, AutoGenerateField = true)]
        [MessagePack.Key("menuFunctionId")]
        public int MenuFunctionId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 用户Id_名称
        /// </summary>
		public const string UserId_Name = "UserId";

		/// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty("userId")]
        [Required]

        [DisplayName("用户Id")]
        [Display(Name = "用户Id", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("userId")]
        public int UserId
        {
            get;
            set;
        }
    }
}
