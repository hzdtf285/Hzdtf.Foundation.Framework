using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 转换类型值接口
    /// @ 黄振东
    /// </summary>
    public interface IConvertTypeValue
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        object To(object value, Type targetType);
    }
}
