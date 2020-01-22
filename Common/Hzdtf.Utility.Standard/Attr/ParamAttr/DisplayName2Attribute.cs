using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr.ParamAttr
{
    /// <summary>
    /// 显示名2特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DisplayName2Attribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        private readonly string name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => name;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        public DisplayName2Attribute(string name) => this.name = name;
    }
}
