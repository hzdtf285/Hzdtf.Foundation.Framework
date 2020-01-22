using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ConnectionInfo
    {
        /// <summary>
        /// 主机名
        /// </summary>
        [JsonProperty("host")]
        [MessagePack.Key("host")]
        public string Host
        {
            get;
            set;
        } = "127.0.0.1";

        /// <summary>
        /// 端口
        /// </summary>
        [JsonProperty("port")]
        [MessagePack.Key("port")]
        public int Port
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty("user")]
        [MessagePack.Key("user")]
        public string User
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        [MessagePack.Key("password")]
        public string Password
        {
            get;
            set;
        }
    }
}
