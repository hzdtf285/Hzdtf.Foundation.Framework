using Castle.DynamicProxy;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Vali;
using Hzdtf.Utility.Standard.Model.Return;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 验证拦截器
    /// @ 黄振东
    /// </summary>
    public class ValiInterceptor : AttrInterceptorBase<ValiAttribute>
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected override void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, ValiAttribute attr, out bool isExecProceeded)
        {
            isExecProceeded = false;
            if (attr.Handlers.IsNullOrLength0())
            {
                return;
            }

            for (int i = 0; i < attr.Handlers.Length; i++)
            {
                Type type = attr.Handlers[i];
                IValiHandler vali = type.CreateInstance<IValiHandler>() as IValiHandler;
                if (vali == null)
                {
                    basicReturn.SetFailureMsg($"{type.FullName}未实现IValiable接口");
                    return;
                }

                BasicReturnInfo reInfo = vali.Exec(invocation.Arguments, attr.Indexs[i]);
                if (basicReturn.Failure())
                {
                    basicReturn.FromBasic(reInfo);
                    return;
                }
            }
        }
    }
}
