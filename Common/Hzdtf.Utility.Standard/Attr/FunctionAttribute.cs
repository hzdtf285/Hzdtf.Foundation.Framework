using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 功能特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionAttribute : MuliCodeAttributeBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        public FunctionAttribute(params string[] code)
            : base(code) { }
    }
}
