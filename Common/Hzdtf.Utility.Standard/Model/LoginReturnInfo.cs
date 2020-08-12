using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 登录返回信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class LoginReturnInfo
    {
        /// <summary>
        /// 是否需要验证码
        /// </summary>
        [JsonProperty("isVerificationCode")]
        [MessagePack.Key("isVerificationCode")]
        public bool IsVerificationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 令牌
        /// </summary>
        [JsonProperty("token")]
        [MessagePack.Key("token")]
        public string Token
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
