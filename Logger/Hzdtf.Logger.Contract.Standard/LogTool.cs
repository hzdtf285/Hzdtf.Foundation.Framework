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
    }
}
