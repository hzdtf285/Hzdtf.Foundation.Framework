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
    public class ConfigLogRecordLevel : ILogRecordLevel
    {
        /// <summary>
        /// 等级
        /// </summary>
        private static string level;

        /// <summary>
        /// 同步等级
        /// </summary>
        private static readonly object syncLevel = new object();

        /// <summary>
        /// 是否设置
        /// </summary>
        private static bool isSet = false;

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public string GetRecordLevel()
        {
            if (isSet)
            {
                return level;
            }

            if (string.IsNullOrWhiteSpace(AppConfig["Logging:LogLevel:Default"]))
            {
                if (string.IsNullOrWhiteSpace(AppConfig["HzdtfLog:LogLevel:Default"]))
                {
                    ILogRecordLevel logLevel = new DefaultLogRecordLevel();
                    SetRecordLevel(logLevel.GetRecordLevel());
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

            return level;
        }

        /// <summary>
        /// 设置记录级别
        /// </summary>
        /// <param name="level">记录级别</param>
        public void SetRecordLevel(string level)
        {
            lock (syncLevel)
            {
                ConfigLogRecordLevel.level = level;
                ConfigLogRecordLevel.isSet = true;
            }
        }
    }
}
