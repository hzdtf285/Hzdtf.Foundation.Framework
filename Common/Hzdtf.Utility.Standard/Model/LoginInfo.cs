using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 登录信息
    /// </summary>
    [MessagePackObject]
    public class LoginInfo
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        [JsonProperty("loginId")]
        [DisplayName("登录ID")]
        [MaxLength(20)]
        [Required]
        [MessagePack.Key("loginId")]
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        [DisplayName("密码")]
        [Required]
        [MaxLength(20)]
        [MessagePack.Key("password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        [JsonProperty("verificationCode")]
        [MessagePack.Key("verificationCode")]
        public string VerificationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 返回路径
        /// </summary>
        [JsonProperty("returnUrl")]
        [MessagePack.Key("returnUrl")]
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}
