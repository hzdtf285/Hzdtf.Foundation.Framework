using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Proxy
{
    /// <summary>
    /// 业务动态代理接口
    /// @ 黄振东
    /// </summary>
    public interface IBusinessDispatchProxy
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>类型实例</returns>
        T Create<T>() where T : class;

       //void SetAsyncT<T>();
    }
}
