using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 负载均衡接口
    /// @ 黄振东
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>元素</returns>
        string Resolve(string[] array);
    }
}
