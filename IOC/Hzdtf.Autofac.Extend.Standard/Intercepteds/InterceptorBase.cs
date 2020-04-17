using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 拦截器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="AttrT">特性类型</typeparam>
    public abstract class InterceptorBase : IInterceptor
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        public void Intercept(IInvocation invocation)
        {
            bool isExecProceeded;
            BasicReturnInfo returnInfo = InterceptOperation(invocation, out isExecProceeded);
            if (returnInfo.Success())
            {
                if (isExecProceeded)
                {
                    return;
                }

                invocation.Proceed();
                return;
            }

            SetErrMsg(invocation, returnInfo);
        }

        /// <summary>
        /// 拦截操作
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        /// <param name="isExecProceeded">是否已执行</param>
        /// <returns>基本返回信息</returns>
        protected abstract BasicReturnInfo InterceptOperation(IInvocation invocation, out bool isExecProceeded);

        /// <summary>
        /// 设置错误消息
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        /// <param name="returnInfo">返回信息</param>
        private void SetErrMsg(IInvocation invocation, BasicReturnInfo returnInfo)
        {
            if (invocation.Method.ReturnType.IsReturnType())
            {
                BasicReturnInfo basicReturn = invocation.Method.ReturnType.CreateInstance<BasicReturnInfo>();
                basicReturn.FromBasic(returnInfo);

                invocation.ReturnValue = basicReturn;

                return;
            }
            else
            {
                throw new Exception(returnInfo.Msg);
            }
        }
    }
}
