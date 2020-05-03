using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 负载均衡类型
    /// @ 黄振东
    /// </summary>
    public static class LoadBalanceType
    {
        /// <summary>
        /// 随机
        /// </summary>
        public static readonly RandomLoadBalance Random = new RandomLoadBalance();

        /// <summary>
        /// 轮询
        /// </summary>
        public static readonly RoundRobinLoadBalance RoundRobin = new RoundRobinLoadBalance();

        /// <summary>
        /// 哈希IP+端口
        /// </summary>
        public static readonly HashIpPortLoadBalance HashIpPort = new HashIpPortLoadBalance();
    }
}
