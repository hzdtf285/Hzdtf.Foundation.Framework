using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.LoadBalance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 服务生成
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ServicesBuilder : IServicesBuilder
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
        /// 异步生成Uri
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>生成Uri任务</returns>
        public async Task<Uri> BuilderAsync(string path)
        {
            var addresses = await ServiceProvider.GetAddresses(ServiceName, Tag);
            var address = LoadBalance.Resolve(addresses);
            var baseUri = new Uri($"{Sheme}://{address}");

            return new Uri(baseUri, path);
        }
    }
}
