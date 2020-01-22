using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.User
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
    }
}
