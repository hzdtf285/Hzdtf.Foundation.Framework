using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// DES加密
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DES : ISymmetricalEncryption
    {
        /// <summary>
        /// KEY
        /// </summary>
        private readonly string key = "0ef69^%$";

        /// <summary>
        /// 向量
        /// </summary>
        private readonly string iv = ";57`QDe?";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">KEY</param>
        /// <param name="iv">向量</param>
        public DES(string key = null, string iv = null)
        {
            this.key = key;
            this.iv = iv;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string plaintext) => DESUtil.Encrypt(plaintext, key, iv);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext ">密文</param>
        /// <returns>解密后的字符串</returns>
        public string Decrypt(string ciphertext) => DESUtil.Decrypt(ciphertext, key, iv);
    }
}
