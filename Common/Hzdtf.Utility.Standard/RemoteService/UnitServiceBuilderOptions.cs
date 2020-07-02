using Hzdtf.Utility.Standard.RemoteService.Builder;
using Hzdtf.Utility.Standard.RemoteService.Options;
using Hzdtf.Utility.Standard.RemoteService.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 统一服务生成配置
    /// @ 黄振东
    /// </summary>
    public class UnitServiceBuilderOptions
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        public UnityServicesOptions ServicesOptions
        {
            get;
            set;
        }

        /// <summary>
        /// 统一服务配置
        /// </summary>
        public IUnityServicesOptions UnityServicesOptions
        {
            get;
            set;
        }

        /// <summary>
        /// 服务生成配置Json文件
        /// </summary>
        public string ServiceBuilderConfigJsonFile
        {
            get;
            set;
        } = "Config/serviceBuilderConfig.json";

        /// <summary>
        /// 统一服务生成器
        /// </summary>
        public IUnityServicesBuilder UnityServicesBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServicesProvider ServiceProvider
        {
            get;
            set;
        }
    }
}
