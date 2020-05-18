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
        /// 跟踪
        /// </summary>
        TRACE = 0,

        /// <summary>
        /// 调试
        /// </summary>
        DEBUG = 1,

        /// <summary>
        /// 信息
        /// </summary>
        INFO = 2,

        /// <summary>
        /// 警告
        /// </summary>
        WRAN = 3,

        /// <summary>
        /// 错误
        /// </summary>
        ERROR = 4,

        /// <summary>
        /// 致命
        /// </summary>
        FATAL = 5,

        /// <summary>
        /// 无
        /// </summary>
        NONE = 6
    }
}
