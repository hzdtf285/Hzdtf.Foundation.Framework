using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 验证参数接口
    /// @ 黄振东
    /// </summary>
    public interface IValiParam
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        string Exec(object value, ValidationAttribute valiAttr, string displayName);
    }
}
