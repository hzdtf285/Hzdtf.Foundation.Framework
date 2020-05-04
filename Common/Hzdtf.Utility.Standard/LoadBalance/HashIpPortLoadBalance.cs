using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Safety;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.LoadBalance
{
    /// <summary>
    /// 哈希IP+端口负载均衡
    /// 如果要加上端口，则在静态属性里设置GetPort
    /// @ 黄振东
    /// </summary>
    public class HashIpPortLoadBalance : LoadBalanceBase
    {
        /// <summary>
        /// 哈希，默认是MD5哈希算法
        /// </summary>
        public static IHash Hash
        {
            get;
            set;
        } = new MD5Hash();

        /// <summary>
        /// 获取端口方法
        /// </summary>
        public static Func<int> GetPort
        {
            get;
            set;
        }

        /// <summary>
        /// 同步本地哈希值
        /// </summary>
        private static readonly object syncLocalHashCode = new object();

        /// <summary>
        /// 本地哈希值
        /// </summary>
        private static long localHashCode = -1;

        /// <summary>
        /// 本地哈希值
        /// </summary>
        private static long LocalHashCode
        {
            get
            {
                if (localHashCode == -1)
                {
                    int port = GetPort == null ? 0 : GetPort();
                    var str = $"{NetworkUtil.LocalIP}:{port}";
                    lock (syncLocalHashCode)
                    {
                        localHashCode = Hash.GenerateHashCode(str);
                    }
                }

                return localHashCode;
            }
        }

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>索引</returns>
        public override int GetIndex(string[] array) => Convert.ToInt32(LocalHashCode % array.Length);
    }
}
