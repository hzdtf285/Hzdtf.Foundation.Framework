using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Proxy
{
    /// <summary>
    /// 业务动态代理基类
    /// 注意：此类会自动判断代理的是否异步方法（返回类型为Task），如果是则自动开启异步，Task返回值必须是object类型，否则会报错
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="SubClassT">子类类型</typeparam>
    public abstract class BusinessDispatchProxyBase<SubClassT> : DispatchProxy, IBusinessDispatchProxy
        where SubClassT : BusinessDispatchProxyBase<SubClassT>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>类型实例</returns>
        public virtual T Create<T>() where T : class => Create<T, SubClassT>();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="targetMethod">目标方法</param>
        /// <param name="args">方法参数数组</param>
        /// <returns>方法返回值</returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            if (targetMethod == null)
            {
                throw new MissingMethodException("无效的方法");
            }

            return targetMethod.IsMethodReturnTask() ? InvokeBusinessAsync(targetMethod, args) : InvokeBusiness(targetMethod, args);
        }

        /// <summary>
        /// 执行业务
        /// </summary>
        /// <param name="targetMethod">目标方法</param>
        /// <param name="args">方法参数数组</param>
        /// <returns>业务返回值</returns>
        protected abstract object InvokeBusiness(MethodInfo targetMethod, object[] args);

        /// <summary>
        /// 异步执行业务
        /// </summary>
        /// <param name="targetMethod">目标方法</param>
        /// <param name="args">方法参数数组</param>
        /// <returns>任务</returns>
        protected virtual Task<object> InvokeBusinessAsync(MethodInfo targetMethod, object[] args)
        {
            if (targetMethod.ReturnType.IsTypeGenericityTask() && !typeof(Task<object>).FullName.Equals(targetMethod.ReturnType.FullName))
            {
                throw new NotSupportedException("不支持返回值是非Task<object>类型的异步方法");
            }

            return Task<object>.Run(() => InvokeBusiness(targetMethod, args));
        }
    }
}
