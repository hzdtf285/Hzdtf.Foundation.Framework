using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 负载均衡基类
    /// @ 黄振东
    /// </summary>
    public abstract class LoadBalanceBase : ILoadBalance
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>元素</returns>
        public string Resolve(string[] array)
        {
            if (array.IsNullOrLength0())
            {
                throw new ArgumentNullException("数组不能为null或长度不能为0");
            }

            return array[GetIndex(array)];
        }

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>索引</returns>
        public abstract int GetIndex(string[] array);
    }
}
