using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 不为空验证参数
    /// @ 黄振东
    /// </summary>
    public class RequiredValiParam : ValiParamBase<RequiredAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, RequiredAttribute valiAttr, string displayName)
        {
            if (valiAttr.AllowEmptyStrings)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(valiAttr.ErrorMessage))
            {
                valiAttr.ErrorMessage = "{0} 是必填的";
            }

            return value == null || string.IsNullOrWhiteSpace(value.ToString()) ? valiAttr.FormatErrorMessage(displayName) : null;
        }
    }
}
