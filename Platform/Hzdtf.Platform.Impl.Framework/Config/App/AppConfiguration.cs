using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Hzdtf.Platform.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.Platform.Impl.Framework.Config.App
{
    /// <summary>
    /// 应用配置
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class AppConfiguration : AppConfigurationBase
    {
        #region 重写父类的方法

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override string this[string key] { get => ConfigurationManager.AppSettings[key]; }

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public override string GetDirectConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name] != null ? ConfigurationManager.ConnectionStrings[name].ConnectionString : null;
        }

        #endregion
    }
}
