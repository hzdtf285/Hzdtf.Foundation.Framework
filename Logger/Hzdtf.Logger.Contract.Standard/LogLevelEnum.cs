using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 日志级别枚举
    /// @ 黄振东
    /// </summary>
    public enum LogLevelEnum : byte
    {
        /// <summary>
        /// 调试
        /// </summary>
        DEBUG = 0,

        /// <summary>
        /// 信息
        /// </summary>
        INFO = 1,

        /// <summary>
        /// 警告
        /// </summary>
        WRAN = 2,

        /// <summary>
        /// 错误
        /// </summary>
        ERROR = 3,

        /// <summary>
        /// 致命
        /// </summary>
        FATAL = 4
    }
}
