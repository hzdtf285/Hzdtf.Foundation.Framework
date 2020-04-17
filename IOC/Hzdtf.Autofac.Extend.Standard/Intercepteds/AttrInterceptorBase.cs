using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 特性拦截器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="AttrT">特性类型</typeparam>
    public abstract class AttrInterceptorBase<AttrT> : InterceptorBase
        where AttrT : Attribute
    {
        /// <summary>
        /// 拦截操作
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        /// <param name="isExecProceeded">是否已执行</param>
        /// <returns>基本返回信息</returns>
        protected override BasicReturnInfo InterceptOperation(IInvocation invocation, out bool isExecProceeded)
        {
            BasicReturnInfo basicReturn = new BasicReturnInfo();
            isExecProceeded = false;
            AttrT attr = invocation.Method.GetAttribute<AttrT>();
            if (attr == null)
            {
                return basicReturn;
            }

            Intercept(basicReturn, invocation, attr, out isExecProceeded);

            return basicReturn;
        }

        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected abstract void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, AttrT attr, out bool isExecProceeded);
    }
}
