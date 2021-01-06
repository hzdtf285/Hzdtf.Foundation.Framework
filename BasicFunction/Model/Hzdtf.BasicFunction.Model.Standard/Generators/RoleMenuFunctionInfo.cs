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
    /// 角色菜单功能信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class RoleMenuFunctionInfo : PersonTimeInfo<int>
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
        /// 角色Id_名称
        /// </summary>
		public const string RoleId_Name = "RoleId";

		/// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("roleId")]
        [Required]

        [DisplayName("角色Id")]
        [Display(Name = "角色Id", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("roleId")]
        public int RoleId
        {
            get;
            set;
        }
    }
}
