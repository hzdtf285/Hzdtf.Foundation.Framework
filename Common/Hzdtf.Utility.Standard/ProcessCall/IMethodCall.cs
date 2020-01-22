using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// 方法调用接口
    /// @ 黄振东
    /// </summary>
    public interface IMethodCall
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        object Invoke(string fullPath, params object[] parames);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <param name="method">方法</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        object Invoke(string fullPath, out MethodInfo method, params object[] parames);
    }
}
