using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 多个编码特性基类
    /// @ 黄振东
    /// </summary>
    public abstract class MuliCodeAttributeBase : Attribute
    {
        /// <summary>
        /// 编码集合
        /// </summary>
        private readonly string[] codes;

        /// <summary>
        /// 编码集合
        /// </summary>
        public string[] Codes
        {
            get => codes;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        public MuliCodeAttributeBase(params string[] code)
        {
            this.codes = code;
        }
    }
}
