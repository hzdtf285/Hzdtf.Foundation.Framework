using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;

namespace Hzdtf.Logger.Integration.ENLog.Standard
{
    /// <summary>
    /// 集成NLog
    /// 需要在应用程序根目录下创建nlog.config配置文件
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IntegrationNLog : LogBase
    {
        /// <summary>
        /// NLog
        /// </summary>
        private readonly NLog.Logger nlog;

        /// <summary>
        /// 名称
        /// </summary>
        private readonly string name;

        /// <summary>
        /// 构造方法
        /// </summary>
        public IntegrationNLog()
            : this(typeof(IntegrationNLog).Name)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        public IntegrationNLog(string name)
        {
            this.name = name;
            nlog = NLog.LogManager.GetLogger(name);
        }

        /// <summary>
        /// 将消息与异常写入到存储设备里
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        protected override void WriteStorage(string level, string msg, Exception ex = null, string source = null, params string[] tags)
        {
            var logger = string.IsNullOrWhiteSpace(source) || name.Equals(source) ? nlog : NLog.LogManager.GetLogger(source);
            if (!tags.IsNullOrLength0())
            {
                msg += " 标签:" + JsonUtil.SerializeIgnoreNull(tags);
            }

            var levelEnum = LogLevelHelper.Parse(level);
            switch (levelEnum)
            {
                case LogLevelEnum.TRACE:
                    logger.Trace(ex, msg);

                    break;

                case LogLevelEnum.DEBUG:
                    logger.Debug(ex, msg);

                    break;

                case LogLevelEnum.INFO:
                    logger.Info(ex, msg);

                    break;

                case LogLevelEnum.WRAN:
                    logger.Warn(ex, msg);

                    break;

                case LogLevelEnum.ERROR:
                    logger.Error(ex, msg);

                    break;

                case LogLevelEnum.FATAL:
                    logger.Fatal(ex, msg);

                    break;
            }
        }
    }
}
