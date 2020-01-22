using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// RPC数据信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RpcDataInfo
    {
        /// <summary>
        /// 方法全路径
        /// </summary>
        [JsonProperty("methodFullPath")]
        [Key("methodFullPath")]
        public string MethodFullPath
        {
            get;
            set;
        }

        /// <summary>
        /// 方法的参数数组
        /// </summary>
        [JsonProperty("methodParams")]
        [Key("methodParams")]
        public object[] MethodParams
        {
            get;
            set;
        }
    }
}
