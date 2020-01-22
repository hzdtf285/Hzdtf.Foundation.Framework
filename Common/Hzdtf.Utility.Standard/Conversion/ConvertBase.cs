using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 转换基类 
    /// @ 黄振东
    /// </summary>
    public abstract class ConvertBase : IConvertable
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        public object To(object value) => value == null ? null : ToNew(value);

        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected abstract object ToNew(object value);
    }
}
