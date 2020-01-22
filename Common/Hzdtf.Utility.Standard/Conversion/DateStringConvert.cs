using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 日期字符串转换
    /// @ 黄振东
    /// </summary>
    public class DateStringConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => ((DateTime)value).ToFixedDate();
    }
}
