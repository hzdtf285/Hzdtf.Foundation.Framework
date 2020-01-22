using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Contract.Standard.Config;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 默认连接字符串工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DefaultConnectionStringFactory : ISimpleFactory<EnvironmentType, string[]>
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public string[] Create(EnvironmentType type)
        {
            string[] result = new string[2];
            switch (type)
            {
                case EnvironmentType.PRODUCTION:
                    result[0] = AppConfig.DefaultConnectionString;
                    result[1] = AppConfig.SlaveConnectionString;

                    break;

                case EnvironmentType.TEST:
                    result[0] = AppConfig.TestDefaultConnectionString;
                    result[1] = AppConfig.TestSlaveConnectionString;

                    break;
            }

            return result;
        }
    }
}
