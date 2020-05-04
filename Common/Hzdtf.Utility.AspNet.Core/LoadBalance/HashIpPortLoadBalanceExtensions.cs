using Hzdtf.Utility.Standard.LoadBalance;
using Hzdtf.Utility.Standard.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.LoadBalance
{
    /// <summary>
    /// 哈希+IP负载均衡扩展类
    /// @ 黄振东
    /// </summary>
    public static class HashIpPortLoadBalanceExtensions
    {
        /// <summary>
        /// 本地端口
        /// </summary>
        private static int localPort;

        /// <summary>
        /// 添加哈希+IP负载均衡
        /// </summary>
        /// <param name="services">服务</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddHashIpPortLoadBalance(this IServiceCollection services)
        {
            // 添加一个能获取本服务地址的服务
            services.AddSingleton(serviceProvider =>
            {
                var server = serviceProvider.GetRequiredService<IServer>();
                return server.Features.Get<IServerAddressesFeature>();
            });

            return services;
        }

        /// <summary>
        /// 使用哈希+IP负载均衡
        /// </summary>
        /// <param name="app">应用生成器</param>
        /// <param name="port">端口</param>
        /// <returns>应用生成器</returns>
        public static IApplicationBuilder UseHashIpPortLoadBalance(this IApplicationBuilder app, int port = 0)
        {
            if (port == 0)
            {
                var add = app.ApplicationServices.GetService<IServerAddressesFeature>().Addresses.FirstOrDefault();
                localPort = NetworkUtil.GetPortFromDomain(NetworkUtil.FilterUrl(add));
            }
            else
            {
                localPort = port;
            }

            HashIpPortLoadBalance.GetPort = () => localPort;

            return app;
        }
    }
}
