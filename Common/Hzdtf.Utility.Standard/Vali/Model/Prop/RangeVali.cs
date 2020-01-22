using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali.Model.Prop
{
    /// <summary>
    /// 范围验证
    /// @ 黄振东
    /// </summary>
    public class RangeVali : PropValiBase<RangeAttribute>
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="value">值</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="valiAttr">验证特性</param>
        /// <returns>基本错误消息</returns>
        protected override string ExecVali(object model, object value, string displayName, RangeAttribute valiAttr)
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
