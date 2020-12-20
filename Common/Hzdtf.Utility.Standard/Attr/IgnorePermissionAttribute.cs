using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 忽略权限特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnorePermissionAttribute : Attribute
    {
    }
}
