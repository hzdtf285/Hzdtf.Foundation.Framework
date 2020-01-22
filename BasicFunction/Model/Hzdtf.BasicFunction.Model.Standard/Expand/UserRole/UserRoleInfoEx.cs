using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 用户角色信息
    /// </summary>
    public partial class UserRoleInfo
    {
        /// <summary>
        /// 角色
        /// </summary>
        [JsonProperty("role")]
        [MessagePack.Key("role")]
        public RoleInfo Role
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty("user")]
        [MessagePack.Key("user")]
        public UserInfo User
        {
            get;
            set;
        }
    }
}
