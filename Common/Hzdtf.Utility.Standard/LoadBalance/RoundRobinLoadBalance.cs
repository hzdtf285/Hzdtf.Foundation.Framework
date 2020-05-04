using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 轮询负载均衡
    /// @ 黄振东
    /// </summary>
    public class RoundRobinLoadBalance : LoadBalanceBase
    {
        /// <summary>
        /// 索引
        /// </summary>
        private int index = -1;

        /// <summary>
        /// 同步对象
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>索引</returns>
        public override int GetIndex(string[] array)
        {
            lock (syncObject)
            {
                if (index >= array.Length - 1)
                {
                    index = -1;
                }

                return ++index;
            }
        }
    }
}
