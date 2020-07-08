using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 负载均衡简单工厂
    /// @ 黄振东
    /// </summary>
    public static class LoadBalanceSimpleFactory
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mode">负载均衡模式</param>
        /// <returns>负载均衡</returns>
        public static ILoadBalance Create(LoadBalanceMode mode)
        {
            switch (mode)
            {
                case LoadBalanceMode.RANDOM:

                    return new RandomLoadBalance();

                case LoadBalanceMode.ROUND_ROBIN:

                    return new RoundRobinLoadBalance();

                case LoadBalanceMode.HASH_IP_PORT:

                    return new HashIpPortLoadBalance();

                default:

                    return null;
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mode">负载均衡模式</param>
        /// <returns>负载均衡</returns>
        public static ILoadBalance Create(string mode)
        {
            if (string.IsNullOrWhiteSpace(mode))
            {
                return null;
            }

            var m = (LoadBalanceMode)Enum.Parse(typeof(LoadBalanceMode), mode);

            return Create(m);
        }
    }
}
