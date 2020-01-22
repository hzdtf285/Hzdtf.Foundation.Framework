using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr.ParamAttr
{
    /// <summary>
    /// 数组不为空特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ArrayNotEmptyAttribute : ValidationAttribute
    {
    }
}
