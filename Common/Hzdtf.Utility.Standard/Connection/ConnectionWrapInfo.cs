using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接包装信息
    /// 优先级顺序：DefaultConnection>ConnectionInfo>ConnectionString>ConnectionStringAppConfigName
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    [MessagePackObject]
    public class ConnectionWrapInfo<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        [JsonProperty("connectionInfo")]
        [MessagePack.Key("connectionInfo")]
        public ConnectionInfoT ConnectionInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [JsonProperty("connectionString")]
        [MessagePack.Key("connectionString")]
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串应用配置名称
        /// </summary>
        [JsonProperty("connectionStringAppConfigName")]
        [MessagePack.Key("connectionStringAppConfigName")]
        public string ConnectionStringAppConfigName
        {
            get;
            set;
        }

        /// <summary>
        /// 默认连接对象
        /// </summary>
        [IgnoreMember]
        [JsonIgnore]
        public IConnection<ConnectionInfoT> DefaultConnection
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 连接包装信息
    /// 优先级顺序：DefaultConnection>ConnectionInfo>ConnectionString>ConnectionStringAppConfigName
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ConnectionWrapInfo : ConnectionWrapInfo<ConnectionInfo>
    {
    }
}
