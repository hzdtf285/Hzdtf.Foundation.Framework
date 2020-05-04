using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 统一服务生成缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class UnityServicesBuilderCache : IUnityServicesBuilder
    {
        /// <summary>
        /// 同步统一服务选项配置
        /// </summary>
        private static readonly object syncOptions = new object();

        /// <summary>
        /// 统一服务选项配置
        /// </summary>
        private static UnityServicesOptions options;

        /// <summary>
        /// 服务提供者
        /// </summary>
        private readonly IServicesProvider servicesProvider;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="servicesProvider">服务提供者</param>
        public UnityServicesBuilderCache(IServicesProvider servicesProvider)
        {
            this.servicesProvider = servicesProvider;
        }

        /// <summary>
        /// 设置统一服务选项配置JSON文件
        /// </summary>
        /// <param name="unityServicesOptionsJsonFile">统一服务选项配置JSON文件</param>
        public static void SetOptionsJsonFile(string unityServicesOptionsJsonFile = "Config/serviceBuilderConfig.json")
        {
            var options = JsonUtil.Deserialize<UnityServicesOptions>(File.ReadAllText(unityServicesOptionsJsonFile));
            SetOptions(options);
        }

        /// <summary>
        /// 设置统一服务选项配置
        /// </summary>
        /// <param name="options">统一服务选项配置</param>
        public static void SetOptions(UnityServicesOptions options)
        {
            lock (syncOptions)
            {
                UnityServicesBuilderCache.options = options;
            }
            UnityServicesBuilderCache.options.Reset();
        }

        /// <summary>
        /// 异步生成地址
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="path">路径</param>
        /// <param name="tag">标签</param>
        /// <returns>生成地址任务</returns>
        public async Task<string> BuilderAsync(string serviceName, string path, string tag = null)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException("服务名不能为空");
            }
            if (options == null)
            {
                throw new ArgumentNullException("统一服务选项配置为null");
            }

            var ser = GetServicesOptions(serviceName, tag);
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

                lock (syncOptions)
                {
                    ser.ServicesBuilder = serviceBuilder;
                }
            }

            return await ser.ServicesBuilder.BuilderAsync(path);
        }

        /// <summary>
        /// 根据服务名和标签获取服务选项配置
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>服务选项配置</returns>
        private ServicesOptions GetServicesOptions(string serviceName, string tag = null)
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
