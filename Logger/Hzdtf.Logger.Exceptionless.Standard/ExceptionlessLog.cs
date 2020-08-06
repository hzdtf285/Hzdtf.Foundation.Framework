using Exceptionless;
using Exceptionless.Logging;
using Hzdtf.Logger.Contract.Standard;
using System;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.Logger.Exceptionless.Standard
{
    /// <summary>
    /// Exceptionless分布式日志
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ExceptionlessLog : LogBase
    {
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static ExceptionlessLog()
        {
            ExceptionlessTool.DefaultInit();
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
            var logLevel = LogLevelHelper.Parse(level);
            var exLevel = LogLevel.Off;
            switch (logLevel)
            {
                case LogLevelEnum.TRACE:
                    exLevel = LogLevel.Trace;

                    break;

                case LogLevelEnum.DEBUG:
                    exLevel = LogLevel.Debug;

                    break;

                case LogLevelEnum.INFO:
                    exLevel = LogLevel.Info;

                    break;

                case LogLevelEnum.WRAN:
                    exLevel = LogLevel.Warn;

                    break;

                case LogLevelEnum.ERROR:
                    exLevel = LogLevel.Error;

                    break;

                case LogLevelEnum.FATAL:
                    exLevel = LogLevel.Fatal;

                    break;
            }

            if (ex != null)
            {
                msg += $".异常:msg:{ex.Message},ex:{ex.StackTrace}";
            }

            var builder = ExceptionlessClient.Default.CreateLog(source, msg, exLevel);
            if (!tags.IsNullOrLength0())
            {
                builder.AddTags(tags);
            }

            builder.Submit();
        }
    }
}
