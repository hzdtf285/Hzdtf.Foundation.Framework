using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 菜单特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute : CodeAttributeBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        public MenuAttribute(string code)
            : base(code) { }
    }
}
