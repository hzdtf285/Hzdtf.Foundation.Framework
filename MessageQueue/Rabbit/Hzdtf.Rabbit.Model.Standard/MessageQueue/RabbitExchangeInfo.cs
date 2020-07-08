using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Rabbit.Model.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Model.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit配置信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RabbitConfigInfo
    {
        /// <summary>
        /// 虚拟路径
        /// </summary>
        [JsonProperty("rabbit")]
        [Key("rabbit")]
        public RabbitVirtualPathInfo[] Rabbit
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Rabbit虚拟路径信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RabbitVirtualPathInfo
    {
        /// <summary>
        /// 虚拟路径
        /// </summary>
        [JsonProperty("virtualPath")]
        [Key("virtualPath")]
        public string VirtualPath
        {
            get;
            set;
        } = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH;

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
        /// 连接应用配置，默认是MessageQueue:RabbitConnectionString
        /// </summary>
        [JsonProperty("connectionStringAppConfigName")]
        [Key("connectionStringAppConfigName")]
        public string ConnectionStringAppConfigName
        {
            get;
            set;
        } = "MessageQueue:RabbitConnectionString";

        /// <summary>
        /// 交换机列表
        /// </summary>
        [JsonProperty("exchanges")]
        [Key("exchanges")]
        public IList<RabbitExchangeInfo> Exchanges
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Rabbit交换机信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RabbitExchangeInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("type")]
        [Key("type")]
        public string Type
        {
            get;
            set;
        } = RabbitConfigUtil.DIRECT_EXCHANGE_NAME;

        /// <summary>
        /// 持久化
        /// </summary>
        [JsonProperty("persistent")]
        [Key("persistent")]
        public bool Persistent
        {
            get;
            set;
        }

        /// <summary>
        /// 队列集合
        /// </summary>
        [JsonProperty("queues")]
        [Key("queues")]
        public IList<RabbitQueueModel> Queues
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Rabbit队列 
    /// </summary>
    [MessagePackObject]
    public class RabbitQueueModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 路由键集合
        /// </summary>
        [JsonProperty("routingKeys")]
        [Key("routingKeys")]
        public string[] RoutingKeys
        {
            get;
            set;
        }

        /// <summary>
        /// 自动删除队列
        /// </summary>
        [JsonProperty("autoDelQueue")]
        [Key("autoDelQueue")]
        public bool AutoDelQueue
        {
            get;
            set;
        }

        /// <summary>
        /// 接收质量数
        /// </summary>
        [JsonProperty("qos")]
        [Key("qos")]
        public ushort? Qos
        {
            get;
            set;
        }

        /// <summary>
        /// RPC客户端程序集数组
        /// </summary>
        [JsonProperty("rpcClientAssemblys")]
        [Key("rpcClientAssemblys")]
        public RpcClientAssemblyInfo[] RpcClientAssemblyInfos
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Rpc客户端程序集信息
    /// @ 黄振东
    /// </summary>
    public class RpcClientAssemblyInfo
    { 
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型，支持json和messagePack，默认为json
        /// </summary>
        [JsonProperty("dataType")]
        [Key("dataType")]
        public string DataType
        {
            get;
            set;
        } = "json";
    }
}
