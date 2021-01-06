using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 菜单功能特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuFunctionAttribute : Attribute
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        private readonly string menuCode;

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode
        {
            get => menuCode;
        }

        /// <summary>
        /// 功能编码
        /// </summary>
        private readonly string functionCode;

        /// <summary>
        /// 功能编码
        /// </summary>
        public string FunctionCode
        {
            get => functionCode;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCode">功能编码</param>
        public MenuFunctionAttribute(string menuCode, string functionCode)
        {
            this.menuCode = menuCode;
            this.functionCode = functionCode;
        }
    }
}
