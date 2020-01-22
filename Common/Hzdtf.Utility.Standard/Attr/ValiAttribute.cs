using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 验证特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ValiAttribute : Attribute
    {
        /// <summary>
        /// 索引位置集合
        /// </summary>
        public byte[] Indexs
        {
            get;
            set;
        }

        /// <summary>
        /// 处理集合
        /// </summary>
        public Type[] Handlers
        {
            get;
            set;
        }
    }
}
