using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 验证参数基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ValiAttrT">验证特性类型</typeparam>
    public abstract class ValiParamBase<ValiAttrT> : IValiParam
        where ValiAttrT : ValidationAttribute
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        public string Exec(object value, ValidationAttribute valiAttr, string displayName)
        {
            if (valiAttr is ValiAttrT)
            {
                return ExecOper(value, (ValiAttrT)valiAttr, displayName);
            }

            return null;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected abstract string ExecOper(object value, ValiAttrT valiAttr, string displayName);
    }
}
