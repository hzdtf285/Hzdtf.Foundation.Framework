using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.RemoteService;
using Hzdtf.Utility.Standard.SystemV2;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Consul.Extensions.Common.Standard
{
    /// <summary>
    /// 服务提供者内存缓存
    /// 必须手工设置原生服务提供者
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ServicesProviderMemory : IServicesProvider
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly IMemoryCache cache;

        /// <summary>
        /// 原生服务提供者
        /// </summary>
        public virtual IServicesProvider ProtoServicesProvider
        {
            get;
            set;
        }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        protected int cacheExpire = 5;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ServicesProviderMemory() : this(5) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cacheExpire">缓存失效时间（单位：秒），-1为永不过期</param>
        public ServicesProviderMemory(int cacheExpire)
            : this(new MemoryCache(new MemoryCacheOptions()
            {
                Clock = new LocalSystemClock()
            }))
        {
            this.cacheExpire = cacheExpire;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cache">缓存</param>
        public ServicesProviderMemory(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// 异步根据服务名获取地址数组
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>地址数组任务</returns>
        public async Task<string[]> GetAddresses(string serviceName, string tag = null)
        {
            return await cache.GetOrCreateAsync<string[]>($"{serviceName}_{tag}", entry =>
            {
                if (cacheExpire != -1)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheExpire);
                }

                return ProtoServicesProvider.GetAddresses(serviceName, tag);
            });
        }
    }
}
