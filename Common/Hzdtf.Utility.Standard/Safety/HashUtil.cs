using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// 哈希辅助类
    /// @ 黄振东
    /// </summary>
    public static class HashUtil
    {
        /// <summary>
        /// 一致性哈希取余数
        /// </summary>
        /// <param name="digest">字节数组</param>
        /// <returns>哈希值</returns>
        public static long ConsistentHash(byte[] digest)
        {
            return (((long)(digest[15] & 0xFF) << 24)
                    | ((long)(digest[14] & 0xFF) << 16)
                    | ((long)(digest[13] & 0xFF) << 8)
                    | (digest[12] & 0xFF))
                    & 0xFFFFFFFFL;
        }

        /// <summary>
        /// 一致性哈希取余数(MD5)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>哈希值</returns>
        public static long ConsistentHashMD5(string text)
        {
            return ConsistentHash(MD5Util.GenerateMD5Bytes(text));
        }
    }
}
