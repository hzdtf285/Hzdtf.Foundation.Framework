using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Menu
{
    /// <summary>
    /// 菜单树信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class MenuTreeInfo : SimpleInfo<int>
    {
        /// <summary>
        /// 功能菜单ID
        /// </summary>
        [JsonProperty("menuFunctionId")]
        [MessagePack.Key("menuFunctionId")]
        public int MenuFunctionId
        {
            get;
            set;
        }

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 文本
        /// </summary>
        [JsonProperty("text")]
        [MessagePack.Key("text")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// 默认为菜单
        /// </summary>
        [JsonProperty("type")]
        [MessagePack.Key("type")]
        public MenuFunctionType Type
        {
            get;
            set;
        } = MenuFunctionType.MENU;

        /// <summary>
        /// 子菜单树信息列表
        /// </summary>
        [JsonProperty("children")]
        [MessagePack.Key("children")]
        public IList<MenuTreeInfo> Children
        {
            get;
            set;
        } = new List<MenuTreeInfo>();
    }

    /// <summary>
    /// 菜单功能类型
    /// </summary>
    public enum MenuFunctionType : byte
    {
        /// <summary>
        /// 菜单
        /// </summary>
        MENU = 1,

        /// <summary>
        /// 功能
        /// </summary>
        FUNCTION = 2
    }
}
