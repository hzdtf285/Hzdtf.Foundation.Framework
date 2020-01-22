using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion.ReverConversion
{
    /// <summary>
    /// 布尔文本反转换
    /// @ 黄振东
    /// </summary>
    public class BoolTextReverConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value)
        {
            string str = value.ToString().ToLower();
            switch (str)
            {
                case "是":
                case "true":
                case "t":
                case "1":

                    return true;

                case "否":
                case "false":
                case "f":
                case "0":

                    return false;

                default:

                    return null;
            }
        }
    }
}
