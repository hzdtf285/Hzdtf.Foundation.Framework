using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Contract.Standard.Config;
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
    public class ConfigLogRecordLevel : ILogRecordLevel
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
        /// 级别
        /// </summary>
        private static string level;

        /// <summary>
        /// 同步级别
        /// </summary>
        private static readonly object syncLevel = new object();

        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public string GetRecordLevel()
        {
            if (string.IsNullOrWhiteSpace(level))
            {
                if (string.IsNullOrWhiteSpace(AppConfig["Logging:LogLevel:Default"]))
                {
                    if (string.IsNullOrWhiteSpace(AppConfig["HzdtfLog:LogLevel:Default"]))
                    {
                        ILogRecordLevel logLevel = new DefaultLogRecordLevel();
                        lock (syncLevel)
                        {
                            level = logLevel.GetRecordLevel();
                        }
                    }
                    else
                    {
                        lock (syncLevel)
                        {
                            level = AppConfig["HzdtfLog:LogLevel:Default"];
                        }
                    }
                }
                else
                {
                    lock (syncLevel)
                    {
                        level = AppConfig["Logging:LogLevel:Default"];
                    }
                }
            }

            return level;
        }
    }
}
