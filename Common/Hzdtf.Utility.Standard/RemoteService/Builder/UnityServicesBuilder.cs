using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.RemoteService.Options;
using Hzdtf.Utility.Standard.RemoteService.Provider;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.RemoteService.Builder
{
    /// <summary>
    /// 统一服务生成
    /// @ 黄振东
    /// </summary>
    public class UnityServicesBuilder : IUnityServicesBuilder
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        private readonly IServicesProvider servicesProvider;

        /// <summary>
        /// 统一服务配置
        /// </summary>
        private readonly IUnityServicesOptions unityServicesOptions;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="servicesProvider">服务提供者</param>
        /// <param name="unityServicesOptions">统一服务配置</param>
        public UnityServicesBuilder(IServicesProvider servicesProvider, IUnityServicesOptions unityServicesOptions)
        {
            this.servicesProvider = servicesProvider;
            this.unityServicesOptions = unityServicesOptions;
        }

        /// <summary>
        /// 异步生成地址
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="path">路径</param>
        /// <param name="tag">标签</param>
        /// <returns>生成地址任务</returns>
        public async Task<string> BuilderAsync(string serviceName, string path = null, string tag = null)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException("服务名不能为空");
            }

            var options = unityServicesOptions.Reader();
            if (options == null)
            {
                throw new ArgumentNullException("统一服务选项配置为null");
            }

            var ser = GetServicesOptions(options, serviceName, tag);
            if (ser == null)
            {
                throw new KeyNotFoundException($"找不到服务名为:{serviceName},标签为:{tag}的配置");
            }

            if (ser.ServicesBuilder == null)
            {
                var serviceBuilder = servicesProvider.CreateServiceBuilder(builder =>
                {
                    builder.LoadBalance = ser.LoadBalance;
                    builder.ServiceName = ser.ServiceName;
                    builder.Tag = ser.Tag;
                    builder.Sheme = ser.Sheme;
                });

                ser.ServicesBuilder = serviceBuilder;
            }

            return await ser.ServicesBuilder.BuilderAsync(path);
        }

        /// <summary>
        /// 根据服务名和标签获取服务选项配置
        /// </summary>
        /// <param name="options">服务配置</param>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>服务选项配置</returns>
        private ServicesOptions GetServicesOptions(UnityServicesOptions options, string serviceName, string tag = null)
        {
            if (options.Services.IsNullOrLength0())
            {
                return null;
            }

            foreach (var ser in options.Services)
            {
                if (ser.ServiceName.Equals(serviceName) && ser.Tag == tag)
                {
                    return ser;
                }
            }

            return null;
        }
    }
}
