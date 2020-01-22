using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard
{
    /// <summary>
    /// 菜单功能信息
    /// @ 黄振东
    /// </summary>
    public partial class MenuFunctionInfo
    {
        /// <summary>
        /// 功能
        /// </summary>
        [JsonProperty("function")]
        [MessagePack.Key("function")]
        public FunctionInfo Function
        {
            get;
            set;
        }
    }
}
