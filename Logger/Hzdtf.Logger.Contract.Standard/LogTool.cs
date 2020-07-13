using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 日志工具
    /// </summary>
    public static class LogTool
    {
        /// <summary>
        /// 默认日志
        /// </summary>
        public static ILogable DefaultLog
        {
            get;
            set;
        } = new ConsoleLog();

        /// <summary>
        /// 获取可用的日志，如果输入为空，则返回DefaultLog
        /// </summary>
        /// <param name="log">日志</param>
        /// <returns>日志</returns>
        public static ILogable AvailableLog(this ILogable log)
        {
            return log == null ? DefaultLog : log;
        }
    }
}
