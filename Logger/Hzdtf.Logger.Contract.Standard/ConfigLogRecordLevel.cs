using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 配置日志记录等级
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ConfigLogRecordLevel : LogRecordLevelBase
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
        /// 获取默认的记录等级
        /// </summary>
        /// <returns>记录级别</returns>
        protected override string GetDefaultRecordLevel()
        {
            if (string.IsNullOrWhiteSpace(AppConfig["Logging:LogLevel:Default"]))
            {
                if (string.IsNullOrWhiteSpace(AppConfig["HzdtfLog:LogLevel:Default"]))
                {
                    ILogRecordLevel logLevel = new DefaultLogRecordLevel();
                    return logLevel.GetRecordLevel();
                }
                else
                {
                    return AppConfig["HzdtfLog:LogLevel:Default"];
                }
            }
            else
            {
               return AppConfig["Logging:LogLevel:Default"];
            }
        }
    }
}
