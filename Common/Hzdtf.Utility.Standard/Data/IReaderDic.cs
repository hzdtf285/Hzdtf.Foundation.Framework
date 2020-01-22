using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 读取字典接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public interface IReaderDic<KeyT, ValueT>
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        ValueT Reader(KeyT key);
    }
}
