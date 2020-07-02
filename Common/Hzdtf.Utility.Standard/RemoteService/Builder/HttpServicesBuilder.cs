using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.LoadBalance;
using Hzdtf.Utility.Standard.RemoteService.Provider;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.RemoteService.Builder
{
    /// <summary>
    /// Http服务生成
    /// @ 黄振东
    /// </summary>
    public class HttpServicesBuilder : IServicesBuilder
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServicesProvider ServiceProvider
        {
            get;
            set;
        }

        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 方案
        /// </summary>
        public string Sheme
        {
            get;
            set;
        } = Uri.UriSchemeHttp;

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// 负载均衡策略
        /// </summary>
        public ILoadBalance LoadBalance
        {
            get;
            set;
        } = LoadBalanceType.Random;

        /// <summary>
        /// 异步生成地址
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>生成地址任务</returns>
        public async Task<string> BuilderAsync(string path = null)
        {
            var addresses = await ServiceProvider.GetAddresses(ServiceName, Tag);
            if (addresses.IsNullOrLength0())
            {
                throw new Exception($"获取服务[{ServiceName}],标签[{Tag}]的地址列表为空");
            }

            var address = LoadBalance.Resolve(addresses);
            var baseUri = new Uri($"{Sheme}://{address}");
            if (string.IsNullOrWhiteSpace(path))
            {
                return baseUri.AbsoluteUri;
            }

            return new Uri(baseUri, path).AbsoluteUri;
        }
    }
}
