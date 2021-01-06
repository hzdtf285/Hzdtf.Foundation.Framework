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
    /// 用户角色信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class UserRoleInfo : PersonTimeInfo<int>
    {
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
        [Display(Name = "角色Id", Order = 8, AutoGenerateField = true)]
        [MessagePack.Key("roleId")]
        public int RoleId
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
