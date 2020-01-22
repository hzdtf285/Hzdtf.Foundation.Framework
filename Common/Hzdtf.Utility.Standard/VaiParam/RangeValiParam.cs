using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 范围验证参数
    /// @ 黄振东
    /// </summary>
    public class RangeValiParam : ValiParamBase<RangeAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, RangeAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }

            double num = Convert.ToDouble(value);
            return num < Convert.ToDouble(valiAttr.Minimum) || num > Convert.ToDouble(valiAttr.Maximum)
                ? valiAttr.FormatErrorMessage(displayName) : null;
        }
    }
}
