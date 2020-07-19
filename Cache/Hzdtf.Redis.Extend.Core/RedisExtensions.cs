using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Redis.Extend.Core
{
    /// <summary>
    /// Redis扩展类
    /// @ 黄振东
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// 添加分布式Redis缓存
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="key">键，默认是Master</param>
        /// <param name="instanceName">实例名，默认是Master</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, string key = "Master", string instanceName = "Master")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Redis键不能为空");
            }
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                throw new ArgumentNullException("实例名不能为空");
            }

            var parse = new RedisConfigOptionParse();
            var config = parse.Parse();
            var keyConfig = config.Get(key);
            if (keyConfig == null)
            {
                throw new KeyNotFoundException($"找不到Redis键[{key}]的配置");
            }

            // 添加分布式Redis缓存
            services.AddDistributedRedisCache(option =>
            {
                //redis 数据库连接字符串
                option.Configuration = config.GetPlaintextConnectionString(keyConfig.ConnectionString);
                //redis 实例名
                option.InstanceName = instanceName;
            }); 

            return services;
        }
    }
}
