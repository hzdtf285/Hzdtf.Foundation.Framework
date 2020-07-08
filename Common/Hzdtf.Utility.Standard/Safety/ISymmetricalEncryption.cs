using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// 对称加密接口
    /// @ 黄振东
    /// </summary>
    public interface ISymmetricalEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>加密后的字符串</returns>
        string Encrypt(string plaintext);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext ">密文</param>
        /// <returns>解密后的字符串</returns>
        string Decrypt(string ciphertext);
    }

    /// <summary>
    /// 加密辅助类
    /// @ 黄振东
    /// </summary>
    public static class SymmetricalEncryptionUtil
    {
        /// <summary>
        /// 默认加密
        /// </summary>
        public static ISymmetricalEncryption DefaultSymmetricalEncryption = new DES();

        /// <summary>
        /// 获取加密，如果传入为空，则返回默认加密
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        /// <returns>加密</returns>
        public static ISymmetricalEncryption GetSymmetricalEncryption(ISymmetricalEncryption symmetricalEncryption = null)
        {
            return symmetricalEncryption == null ? DefaultSymmetricalEncryption : symmetricalEncryption;
        }
    }
}
