using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Core.ExceptionHandle
{
    /// <summary>
    /// API异常处理中间件
    /// 只有对路径是从传过来的配置前辍才处理
    /// 会对下一个中间件捕获异常，如果发生异常，会返回BasicReturnInfo对象
    /// @ 黄振东
    /// </summary>
    public class ApiExceptionHandleMiddleware
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Api异常处理选项配置
        /// </summary>
        private readonly ApiExceptionHandleOptions options;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">Api异常处理选项配置</param>
        /// <param name="log">日志</param>
        public ApiExceptionHandleMiddleware(RequestDelegate next, IOptions<ApiExceptionHandleOptions> options, ILogable log)
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
            var path = context.Request.Path.Value.ToLower();
            if (path.StartsWith(options.PfxApiPath))
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    _ = log.ErrorAsync(ex.Message, ex, "ApiExceptionHandleMiddleware");

                    var returnInfo = new BasicReturnInfo()
                    {
                        Code = options.ExceptionCode,
                        Msg = options.ExceptionMsg,
                    };
                    if (options.IsDevelopment)
                    {
                        returnInfo.Ex = ex;
                        returnInfo.Desc = ex.ToString();
                    }
                    else
                    {
                        returnInfo.Desc = ex.Message;
                    }

                    context.Response.ContentType = "text/html;charset=UTF-8";
                    context.Response.StatusCode = options.HttpStatusCode;

                    await context.Response.WriteAsync(options.Serialization.Serialize(returnInfo));
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
