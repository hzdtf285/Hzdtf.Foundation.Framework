using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 性别值到文本转换
    /// @ 黄振东
    /// </summary>
    public class GenderValueToTextConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => (bool)value ? "男" : "女";
    }

    /// <summary>
    /// 性别文本到值转换
    /// @ 黄振东
    /// </summary>
    public class GenderTextToValueConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => "男".Equals(value);
    }
}
