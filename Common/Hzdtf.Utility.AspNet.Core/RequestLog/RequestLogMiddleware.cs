using Hzdtf.Logger.Contract.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Core.RequestLog
{
    /// <summary>
    /// 请求日志中间件
    /// @ 黄振东
    /// </summary>
    public class RequestLogMiddleware
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 请求日志选项配置
        /// </summary>
        private readonly RequestLogOptions options;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">请求日志选项配置</param>
        /// <param name="log">日志</param>
        public RequestLogMiddleware(RequestDelegate next, IOptions<RequestLogOptions> options, ILogable log)
        {
            this.next = next;
            this.options = options.Value;
            this.log = log;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var stop = new Stopwatch();
            stop.Start();
            var path = context.Request.Path.Value.ToLower();
            await next(context);
            stop.Stop();

            var msg = $"请求:{path},耗时:{stop.ElapsedMilliseconds}ms";
            switch (options.LogLevel)
            {
                case LogLevelEnum.TRACE:
                    _ = log.TraceAsync(msg, null, "RequestLogMiddleware", path);

                    break;

                case LogLevelEnum.DEBUG:
                    _ = log.DebugAsync(msg, null, "RequestLogMiddleware", path);

                    break;

                case LogLevelEnum.WRAN:
                    _ = log.WranAsync(msg, null, "RequestLogMiddleware", path);

                    break;

                case LogLevelEnum.INFO:
                    _ = log.InfoAsync(msg, null, "RequestLogMiddleware", path);

                    break;

                case LogLevelEnum.ERROR:
                    _ = log.ErrorAsync(msg, null, "RequestLogMiddleware", path);

                    break;

                case LogLevelEnum.FATAL:
                    _ = log.FatalAsync(msg, null, "RequestLogMiddleware", path);

                    break;
            }
        }
    }
}
