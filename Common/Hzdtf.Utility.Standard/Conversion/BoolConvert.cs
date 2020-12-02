using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 布尔值到文本转换
    /// @ 黄振东
    /// </summary>
    public class BoolValueToTextConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => (bool)value? "是" : "否";
    }

    /// <summary>
    /// 布尔文本到值转换
    /// @ 黄振东
    /// </summary>
    public class BoolTextToValueConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => "是".Equals(value);
    }
}
