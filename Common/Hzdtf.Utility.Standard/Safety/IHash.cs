using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Safety
{
    /// <summary>
    /// 哈希接口
    /// @ 黄振东
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// 生成哈希值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>哈希值</returns>
        long GenerateHashCode(string key);
    }
}
