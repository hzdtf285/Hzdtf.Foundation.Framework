using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 基本权限信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BasicPermissionInfo
    {
        /// <summary>
        /// 拥有的功能编码列表
        /// 默认已初始化列表
        /// </summary>
        [JsonProperty("haveFunctionCodes")]
        [Key("haveFunctionCodes")]
        public IList<string> HaveFunctionCodes
        {
            get;
            set;
        } = new List<string>();
    }
}