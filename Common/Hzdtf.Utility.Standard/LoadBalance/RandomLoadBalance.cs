using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 随机负载均衡
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class RandomLoadBalance<T> : ILoadBalance<T>
    {
        /// <summary>
        /// 随机
        /// </summary>
        private readonly Random random = new Random();

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

            return array[random.Next(array.Length)];
        }
    }
}
