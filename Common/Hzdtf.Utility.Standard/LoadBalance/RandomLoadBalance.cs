using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 随机负载均衡
    /// @ 黄振东
    /// </summary>
    public class RandomLoadBalance : LoadBalanceBase
    {
        /// <summary>
        /// 随机
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>索引</returns>
        public override int GetIndex(string[] array) => random.Next(array.Length);
    }
}
