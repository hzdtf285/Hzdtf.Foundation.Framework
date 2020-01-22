using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 分到元转换
    /// @ 黄振东
    /// </summary>
    public class FenToYuanConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value) => NumberUtil.FenToYuan((long)value);
    }
}
