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
    /// 修改密码信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class ModifyPasswordInfo : SimpleInfo
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [JsonProperty("newPassword")]
        [Required]
        [DisplayName("新密码")]
        [MessagePack.Key("newPassword")]
        public string NewPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 确认密码
        /// </summary>
        [JsonProperty("confirmPassword")]
        [Required]
        [Compare("NewPassword", ErrorMessage = "{0}与新密码不一致")]
        [DisplayName("确认密码")]
        [MessagePack.Key("confirmPassword")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
