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
    public class HashIpPortLoadBalance : ILoadBalance
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
                    var str = GetPort == null ? NetworkUtil.LocalIP : $"{NetworkUtil.LocalIP}:{GetPort()}";
                    lock (syncLocalHashCode)
                    {
                        localHashCode = Hash.GenerateHashCode(str);
                    }
                }

                return localHashCode;
            }
        }

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
            
            return array[LocalHashCode % array.Length];
        }
    }
}
