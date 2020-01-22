using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// MD5辅助类
    /// @ 黄振东
    /// </summary>
    public static class MD5Util
    {
        /// <summary>
        /// 加密（32位)
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="isToLower">是否转换为小写</param>
        /// <returns>密文</returns>
        public static string Encryption32(string plaintext, bool isToLower = false)
        {
            if (plaintext == null)
            {
                return null;
            }

            byte[] result = Encoding.Default.GetBytes(plaintext);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string str = BitConverter.ToString(output).Replace("-", null);
            if (isToLower)
            {
                return str.ToLower();
            }

            return str;
        }

        /// <summary>
        /// 加密（16位)
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="isToLower">是否转换为小写</param>
        /// <returns>密文</returns>
        public static string Encryption16(string plaintext, bool isToLower = false)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(plaintext)), 4, 8);
            result = result.Replace("-", "");
            if (isToLower)
            {
                return result.ToLower();
            }

            return result;
        }
    }
}
