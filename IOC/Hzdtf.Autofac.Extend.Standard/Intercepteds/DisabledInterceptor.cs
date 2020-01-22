using Castle.DynamicProxy;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 禁用拦截器
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DisabledInterceptor : AttrInterceptorBase<DisabledAttribute>
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected override void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, DisabledAttribute attr, out bool isExecProceeded)
        {
            isExecProceeded = false;
            basicReturn.SetFailureMsg("Sorry,此功能禁止访问");
        }
    }
}
