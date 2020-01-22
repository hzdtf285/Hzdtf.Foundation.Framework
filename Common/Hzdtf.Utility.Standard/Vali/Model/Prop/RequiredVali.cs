using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali.Model.Prop
{
    /// <summary>
    /// 不为空验证
    /// @ 黄振东
    /// </summary>
    public class RequiredVali : PropValiBase<RequiredAttribute>
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="value">值</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="valiAttr">验证特性</param>
        /// <returns>基本错误消息</returns>
        protected override string ExecVali(object model, object value, string displayName, RequiredAttribute valiAttr)
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
