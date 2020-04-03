using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// MD5哈希
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class MD5Hash : IHash
    {
        /// <summary>
        /// 生成哈希值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>哈希值</returns>
        public long GenerateHashCode(string key) => HashUtil.ConsistentHashMD5(key);
    }
}
