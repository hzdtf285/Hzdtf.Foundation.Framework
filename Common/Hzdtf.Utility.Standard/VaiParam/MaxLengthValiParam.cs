using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 最大长度验证参数
    /// @ 黄振东
    /// </summary>
    public class MaxLengthValiParam : ValiParamBase<MaxLengthAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, MaxLengthAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString().Length > valiAttr.Length ? valiAttr.FormatErrorMessage(displayName) : null;
        }
    }
}
