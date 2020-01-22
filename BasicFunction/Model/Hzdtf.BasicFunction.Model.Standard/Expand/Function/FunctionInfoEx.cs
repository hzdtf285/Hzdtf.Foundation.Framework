using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 功能信息
    /// @ 黄振东
    /// </summary>
    public partial class FunctionInfo
    {
        /// <summary>
        /// 菜单功能ID
        /// </summary>
        [JsonProperty("menuFunctionId")]
        [MessagePack.Key("menuFunctionId")]
        public int MenuFunctionId
        {
            get;
            set;
        }
    }
}
