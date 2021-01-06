using Hzdtf.Logger.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.RequestLog
{
    /// <summary>
    /// 请求日志选项
    /// @ 黄振东
    /// </summary>
    public class RequestLogOptions
    {
        /// <summary>
        /// 日志等级，默认是Trace
        /// </summary>
        public LogLevelEnum LogLevel
        {
            get;
            set;
        } = LogLevelEnum.TRACE;
    }
}
