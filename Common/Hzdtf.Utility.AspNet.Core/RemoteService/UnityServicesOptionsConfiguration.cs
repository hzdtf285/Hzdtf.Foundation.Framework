using Hzdtf.Utility.AspNet.Core.Config;
using Hzdtf.Utility.Standard.RemoteService.Options;
using Hzdtf.Utility.Standard.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.RemoteService
{
    /// <summary>
    /// 统一服务配置来自微软配置对象里
    /// @ 黄振东
    /// </summary>
    public class UnityServicesOptionsConfiguration : JsonFileMicrosoftConfigurationBase<UnityServicesOptions>, IUnityServicesOptions
    {
        /// <summary>
        /// 服务配置数组，目的是为了从配置里读取时，能将部分对象还原
        /// </summary>
        private ServicesOptions[] services;

        /// <summary>
        /// 同步服务配置列表
        /// </summary>
        private readonly object syncServices = new object();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public UnityServicesOptionsConfiguration(string jsonFile = "Config/serviceBuilderConfig.json", Action<IConfigurationBuilder> beforeConfigurationBuilder = null) 
            : base(jsonFile, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public UnityServicesOptionsConfiguration(UnityServicesOptions options, Action<IConfigurationBuilder> beforeConfigurationBuilder = null)
            : base(options, beforeConfigurationBuilder)
        {
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public override UnityServicesOptions Reader()
        {
            var options = base.Reader();
            options.Reset();
            if (options != null && !options.Services.IsNullOrLength0() && !services.IsNullOrLength0())
            {
                foreach (var item in options.Services)
                {
                    var oldService = services.Where(p => p.ServiceName == item.ServiceName).FirstOrDefault();
                    // 如果新旧的服务负载均衡不一样，则退出
                    if (oldService == null || oldService.LoadBalanceMode != item.LoadBalanceMode)
                    {
                        continue;
                    }
                    item.LoadBalance = oldService.LoadBalance;
                }
            }
            SetServiceOptions(options.Services);

            return options;
        }

        /// <summary>
        /// 写入到存储里
        /// </summary>
        /// <param name="data">数据</param>
        protected override void WriteToStorage(UnityServicesOptions data)
        {
            SetServiceOptions(data.Services);
            base.WriteToStorage(data);
        }

        /// <summary>
        /// 设置服务配置数组
        /// </summary>
        /// <param name="services">服务配置数组</param>
        private void SetServiceOptions(ServicesOptions[] services)
        {
            lock (syncServices)
            {
                this.services = services;
            }
        }
    }
}
