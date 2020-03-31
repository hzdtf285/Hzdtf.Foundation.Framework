using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 轮询负载均衡
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class PollingLoadBalance<T> : ILoadBalance<T>
    {
        /// <summary>
        /// 索引
        /// </summary>
        private int index = -1;

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>元素</returns>
        public T Resolve(T[] array)
        {
            if (array.IsNullOrLength0())
            {
                throw new ArgumentNullException("数组不能为null或长度不能为0");
            }

            if (index >= array.Length - 1)
            {
                index = -1;
            }

            return array[++index];
        }
    }
}
