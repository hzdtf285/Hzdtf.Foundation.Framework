using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 编码特性基类
    /// @ 黄振东
    /// </summary>
    public abstract class CodeAttributeBase : Attribute
    {
        /// <summary>
        /// 编码
        /// </summary>
        private readonly string code;

        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            get => code;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        public CodeAttributeBase(string code)
        {
            this.code = code;
        }
    }
}
