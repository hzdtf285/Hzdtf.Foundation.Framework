using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Logger.Text.Integration.MicrosoftLog.Standard;
using Hzdtf.Utility.Standard.Attr;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Integration.MicrosoftLog.Standard
{
    /// <summary>
    /// 集成日志提供者
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IntegrationLogProvider : ILoggerProvider
    {
        /// <summary>
        /// 原生日志
        /// </summary>
        public ILogable ProtoLog
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 日志记录级别
        /// </summary>
        public ILogRecordLevel LogRecordLevel
        {
            get;
            set;
        } = new DefaultLogRecordLevel();

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="categoryName">分类名称</param>
        /// <returns>日志</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new IntegrationLog(categoryName, ProtoLog, LogRecordLevel);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }
    }
}
