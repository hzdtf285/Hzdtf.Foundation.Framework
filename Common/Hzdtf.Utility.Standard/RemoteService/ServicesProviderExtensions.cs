using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 服务提供者扩展类
    /// @ 黄振东
    /// </summary>
    public static class ServicesProviderExtensions
    {
        /// <summary>
        /// 创建服务生成器
        /// </summary>
        /// <param name="service">服务提供者</param>
        /// <param name="config">配置回调</param>
        /// <returns>服务生成器</returns>
        public static IServicesBuilder CreateServiceBuilder(this IServicesProvider service, Action<IServicesBuilder> config)
        {
            var builder = new ServicesBuilder();
            builder.ServiceProvider = service;
            config(builder);

            return builder;
        }
    }
}
