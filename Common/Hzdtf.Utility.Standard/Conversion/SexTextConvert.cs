using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 性别文本转换
    /// @ 黄振东
    /// </summary>
    public class SexTextConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value)
        {
            bool boolValue = (bool)value;
            return boolValue ? "男" : "女";
        }
    }
}
