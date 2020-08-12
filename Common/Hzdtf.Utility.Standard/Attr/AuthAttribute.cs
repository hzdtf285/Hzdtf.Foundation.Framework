using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 授权特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthAttribute : Attribute
    {
        /// <summary>
        /// 当前用户位置索引
        /// 默认为-1，为-1时，表示方法没有传当前用户参数
        /// </summary>
        public sbyte CurrUserParamIndex
        {
            get;
            set;
        } = -1;
    }
}
