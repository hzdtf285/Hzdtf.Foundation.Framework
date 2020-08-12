using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 包含用户信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class IncludeUserInfo<UserInfoT> : SimpleInfo where UserInfoT : BasicUserInfo
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        [JsonProperty("currUser")]
        [DisplayName("当前用户")]
        [MessagePack.Key("currUser")]
        public UserInfoT CurrUser
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 包含用户信息
    /// @ 黄振东
    /// </summary>
    public class IncludeUserInfo : IncludeUserInfo<BasicUserInfo>
    {
    }
}
