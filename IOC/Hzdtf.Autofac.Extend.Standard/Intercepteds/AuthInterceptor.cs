using Castle.DynamicProxy;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 授权拦截器
    /// @ 黄振东
    /// </summary>
    public class AuthInterceptor : AttrInterceptorBase<AuthAttribute>
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected override void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, AuthAttribute attr, out bool isExecProceeded)
        {
            isExecProceeded = false;
            var currUser = attr.CurrUserParamIndex == -1 ? null : invocation.Arguments[attr.CurrUserParamIndex];
            //var user = UserTool.GetCurrUser(currUser as BasicUserInfo);
            //if (user == null)
            //{
            //    basicReturn.SetCodeMsg(403, "您还未授权，无权限访问");
            //}
        }
    }
}
