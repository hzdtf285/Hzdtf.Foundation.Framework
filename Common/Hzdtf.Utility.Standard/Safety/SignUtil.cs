using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// 签名辅助类
    /// @ 黄振东
    /// </summary>
    public static class SignUtil
    {
        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="key">KEY</param>
        /// <param name="data">数据</param>
        /// <returns>签名字符串</returns>
        public static string ComputeSignature(string key, string data)
        {
            using (var algorithm = KeyedHashAlgorithm.Create("HmacSHA1".ToUpperInvariant()))
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key.ToCharArray());

                return Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(data.ToCharArray())));
            }
        }
    }
}
