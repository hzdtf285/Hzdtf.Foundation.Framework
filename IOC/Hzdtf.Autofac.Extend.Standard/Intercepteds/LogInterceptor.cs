using Castle.DynamicProxy;
using Hzdtf.Logger.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 日志拦截器
    /// @ 黄振东
    /// </summary>
    public class LogInterceptor : IInterceptor
    {
        /// <summary>
        /// 同步日志
        /// </summary>
        private static readonly object syncLog = new object();

        /// <summary>
        /// 日志
        /// </summary>
        private static ILogable log;

        /// <summary>
        /// 日志
        /// </summary>
        private static ILogable Log
        {
            get
            {
                if (log == null)
                {
                    ILogable tempLog = AutofacTool.Resolve<ILogable>();
                    if (tempLog == null)
                    {
                        lock (syncLog)
                        {
                            log = LogTool.DefaultLog;
                        }
                    }
                    else
                    {
                        lock (syncLog)
                        {
                            log = tempLog;
                        }
                    }
                }

                return log;
            }
        }

        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        public void Intercept(IInvocation invocation)
        {
            LogAttribute logAttr = invocation.Method.GetAttribute<LogAttribute>();
            string paraLog = null;

            if (logAttr != null)
            {
                if (logAttr.ExecProc)
                {
                    if (!logAttr.IgnoreParamValues)
                    {
                        paraLog = $",params:{ string.Join(",", invocation.Arguments)}";
                    }
                }
                else
                {
                    invocation.Proceed();

                    return;
                }
            }
            else
            {
                paraLog = $",params:{ string.Join(",", invocation.Arguments)}";
            }

            DateTime startTime = DateTime.Now;
            StringBuilder logMsg = new StringBuilder($"{invocation.TargetType.FullName} {invocation.Method}{paraLog}");

            try
            {
                invocation.Proceed();
                FilterReturnValue(invocation, null);

                string returnValLog = null;
                if (logAttr == null || !logAttr.IgnoreParamReturn)
                {
                    returnValLog = $"ReturnValue:{invocation.ReturnValue},";
                }

                TimeSpan span = DateTime.Now - startTime;
                logMsg.AppendFormat(",{0}timed:{1}ms", returnValLog, span.TotalMilliseconds);

                Log.Debug(logMsg.ToString(), null, invocation.TargetType.Name);
            }
            catch (Exception ex)
            {
                bool isHandledException = FilterReturnValue(invocation, ex);
                if (isHandledException)
                {
                    logMsg.AppendFormat(",ReturnValue:{0}", invocation.ReturnValue);
                }

                TimeSpan span = DateTime.Now - startTime;
                logMsg.AppendFormat(",timed:{0}ms", span.TotalMilliseconds);

                Log.ErrorAsync(logMsg.ToString(), ex, invocation.TargetType.Name);

                if (isHandledException)
                {
                    return;
                }

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 过滤返回值
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        /// <param name="ex"></param>
        /// <returns>是否已过滤</returns>
        private bool FilterReturnValue(IInvocation invocation, Exception ex)
        {
            if (invocation.Method.ReturnType.IsReturnType())
            {
                if (ex == null)
                {
                    BasicReturnInfo basicReturnInfo;
                    if (invocation.ReturnValue != null)
                    {
                        basicReturnInfo = invocation.ReturnValue as BasicReturnInfo;
                    }
                    else
                    {
                        basicReturnInfo = invocation.Method.ReturnType.CreateInstance<BasicReturnInfo>();
                        invocation.ReturnValue = basicReturnInfo;
                    }
                    if (string.IsNullOrWhiteSpace(basicReturnInfo.Msg))
                    {
                        if (basicReturnInfo.Success())
                        {
                            basicReturnInfo.SetSuccessMsg("操作成功");
                        }
                        else
                        {
                            basicReturnInfo.SetSuccessMsg("操作失败");
                        }
                    }
                }
                else
                {
                    BasicReturnInfo basicReturnInfo = invocation.Method.ReturnType.CreateInstance<BasicReturnInfo>();
                    basicReturnInfo.SetFailureMsg("操作异常，请联系管理员", ex.Message);
                    invocation.ReturnValue = basicReturnInfo;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
