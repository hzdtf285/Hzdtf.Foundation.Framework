using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.User
{
    /// <summary>
    /// 当前用户修改密码信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class CurrUserModifyPasswordInfo : ModifyPasswordInfo
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        [JsonProperty("loginId")]
        [DisplayName("登录ID")]
        [Required]
        [MessagePack.Key("loginId")]
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        /// 旧密码
        /// </summary>
        [JsonProperty("oldPassword")]
        [Required]
        [DisplayName("旧密码")]
        [MessagePack.Key("oldPassword")]
        public string OldPassword
        {
            get;
            set;
        }
    }
}
