using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 可获取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public interface IGetable<KeyT, ValueT>
    {
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        ValueT Get(KeyT key);
    }
}
