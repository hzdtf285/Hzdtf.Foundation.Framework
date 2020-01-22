using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 可转换接口
    /// @ 黄振东
    /// </summary>
    public interface IConvertable
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        object To(object value);
    }
}
