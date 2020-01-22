using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.User
{
    /// <summary>
    /// 用户页面信息
    /// </summary>
    [MessagePackObject]
    public class UserPageInfo : PageInfo
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        [JsonProperty("roles")]
        [MessagePack.Key("roles")]
        public IList<RoleInfo> Roles
        {
            get;
            set;
        }
    }
}
