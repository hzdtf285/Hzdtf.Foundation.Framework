using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.User
{
    /// <summary>
    /// 用户菜单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class UserMenuInfo : UserInfo
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        [JsonProperty("menus")]
        [MessagePack.Key("menus")]
        public IList<MenuInfo> Menus
        {
            get;
            set;
        }
    }
}
