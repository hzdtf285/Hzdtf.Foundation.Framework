using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 转换类型值基类
    /// @ 黄振东
    /// </summary>
    public abstract class ConvertTypeValueBase : IConvertTypeValue
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        public object To(object value, Type targetType) => value == null ? null : ToNew(value, targetType);

        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        protected abstract object ToNew(object value, Type targetType);
    }
}
