using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 菜单信息
    /// @ 黄振东
    /// </summary>
    public partial class MenuInfo
    {
        /// <summary>
        /// 功能列表
        /// </summary>
        [JsonProperty("functions")]
        [MessagePack.Key("functions")]
        public IList<FunctionInfo> Functions
        {
            get;
            set;
        }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        [JsonProperty("children")]
        [MessagePack.Key("children")]
        public IList<MenuInfo> Children
        {
            get;
            set;
        }
    }
}
