using Hzdtf.Platform.Contract.Standard.Config.App;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard.Attr;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Configuration.Impl.Config.Core.App
{
    /// <summary>
    /// 应用配置
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class AppConfiguration : AppConfigurationBase
    {
        #region 属性与字段

        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfiguration config;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public AppConfiguration()
            : this(PlatformCodeTool.Config) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config">配置</param>
        public AppConfiguration(IConfiguration config) => this.config = config;

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override string this[string key] { get => config[key]; }

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public override string GetDirectConnectionString(string name) => config.GetConnectionString(name);

        #endregion
    }
}
