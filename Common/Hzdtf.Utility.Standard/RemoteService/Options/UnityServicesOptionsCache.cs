using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data.Config;
using Hzdtf.Utility.Standard.RemoteService.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.RemoteService.Options
{
    /// <summary>
    /// 统一服务配置缓存
    /// @ 黄振东
    /// </summary>
    public class UnityServicesOptionsCache : JsonFileConfigurationBase<UnityServicesOptions>, IUnityServicesOptions
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
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        public UnityServicesOptionsCache(string jsonFile = "Config/serviceBuilderConfig.json") : base(jsonFile) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">配置</param>
        public UnityServicesOptionsCache(UnityServicesOptions options) : base(options) { }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public override UnityServicesOptions Reader() => options;

        /// <summary>
        /// 写入到存储里
        /// </summary>
        /// <param name="data">数据</param>
        protected override void WriteToStorage(UnityServicesOptions data)
        {
            if (data != null)
            {
                data.Reset();
            }
            lock (syncOptions)
            {
                UnityServicesOptionsCache.options = data;
            }
        }
    }
}
