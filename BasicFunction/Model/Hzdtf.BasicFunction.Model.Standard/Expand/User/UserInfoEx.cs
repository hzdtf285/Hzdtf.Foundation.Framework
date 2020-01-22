using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 用户信息
    /// @ 黄振东
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// 拥有的角色列表
        /// </summary>
        [JsonProperty("ownRoles")]
        [Display(Name = "拥有的角色列表", Order = 0, AutoGenerateField = false)]
        [MessagePack.Key("ownRoles")]
        public IList<RoleInfo> OwnRoles
        {
            get;
            set;
        }
    }
}
