using Hzdtf.Utility.Standard.Connection;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Model.Standard.Connection
{
    /// <summary>
    /// Rabbit连接信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class RabbitConnectionInfo : ConnectionInfo
    {
        /// <summary>
        /// 虚拟路径
        /// </summary>
        [JsonProperty("virtualPath")]
        [MessagePack.Key("virtualPath")]
        public string VirtualPath
        {
            get;
            set;
        } = "/";

        /// <summary>
        /// 心跳包间隔时间（单位：秒），默认为60秒
        /// </summary>
        [JsonProperty("heartbeat")]
        [MessagePack.Key("heartbeat")]
        public ushort Heartbeat
        {
            get;
            set;
        } = 60;

        /// <summary>
        /// 自动恢复连接，默认开启
        /// </summary>
        [JsonProperty("autoRecovery")]
        [MessagePack.Key("autoRecovery")]
        public bool AutoRecovery
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitConnectionInfo() => Port = 5672;

        /// <summary>
        /// 将连接信息转换为Rabbit连接信息，如果转换失败则抛出异常
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        /// <returns>Rabbit连接信息</returns>
        public static RabbitConnectionInfo From(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null)
            {
                return null;
            }

            if (connectionInfo is RabbitConnectionInfo)
            {
                return connectionInfo as RabbitConnectionInfo;
            }

            throw new NullReferenceException("连接信息不能转换为Rabbit连接信息");
        }
    }
}
