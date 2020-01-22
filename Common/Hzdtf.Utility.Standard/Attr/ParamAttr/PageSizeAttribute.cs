using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr.ParamAttr
{
    /// <summary>
    /// 每页记录数特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PageSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 每页最大记录数
        /// </summary>
        private readonly int maxPageSize;

        /// <summary>
        /// 每页最大记录数
        /// </summary>
        public int MaxPageSize
        {
            get => maxPageSize;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageSizeAttribute()
            : this(UtilTool.MaxPageSize) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxPageSize">每页最大记录数</param>
        public PageSizeAttribute(int maxPageSize) => this.maxPageSize = maxPageSize;
    }
}
