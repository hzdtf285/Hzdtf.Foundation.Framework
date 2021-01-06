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
    /// 菜单功能信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class MenuFunctionInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 功能Id_名称
        /// </summary>
		public const string FunctionId_Name = "FunctionId";

		/// <summary>
        /// 功能Id
        /// </summary>
        [JsonProperty("functionId")]
        [Required]

        [DisplayName("功能Id")]
        [Display(Name = "功能Id", Order = 4, AutoGenerateField = true)]
        [MessagePack.Key("functionId")]
        public int FunctionId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 菜单Id_名称
        /// </summary>
		public const string MenuId_Name = "MenuId";

		/// <summary>
        /// 菜单Id
        /// </summary>
        [JsonProperty("menuId")]
        [Required]

        [DisplayName("菜单Id")]
        [Display(Name = "菜单Id", Order = 6, AutoGenerateField = true)]
        [MessagePack.Key("menuId")]
        public int MenuId
        {
            get;
            set;
        }


    }
}
