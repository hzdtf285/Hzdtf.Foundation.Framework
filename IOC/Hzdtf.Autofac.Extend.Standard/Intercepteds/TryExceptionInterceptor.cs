using Castle.DynamicProxy;
using Hzdtf.Logger.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 捕获异常拦截器，方法的返回值必须是BasicReturnInfo或BasicReturnInfo子类，否则会抛出异常
    /// @ 黄振东
    /// </summary>
    public class TryExceptionInterceptor : IInterceptor
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
            var attr = invocation.Method.GetAttribute<NotTryExceptionAttribute>();
            if (attr != null)
            {
                invocation.Proceed();
                return;
            }

            try
            {
                invocation.Proceed();
                FilterReturnValue(invocation, null);
            }
            catch (Exception ex)
            {
                Log.ErrorAsync(ex.Message, ex, invocation.Method.Name);
                if (FilterReturnValue(invocation, ex))
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
