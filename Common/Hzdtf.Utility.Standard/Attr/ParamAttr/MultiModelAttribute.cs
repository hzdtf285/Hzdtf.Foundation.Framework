using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr.ParamAttr
{
    /// <summary>
    /// 多个模型特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class MultiModelAttribute : ValidationAttribute
    {
    }
}
