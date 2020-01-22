using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Enums
{
    /// <summary>
    /// 数据内容类型
    /// @ 黄振东
    /// </summary>
    public enum DataContentType : byte
    {
        /// <summary>
        /// 文本
        /// </summary>
        TEXT = 1,

        /// <summary>
        /// HTML
        /// </summary>
        HTML = 2,

        /// <summary>
        /// JSON
        /// </summary>
        JSON = 3,

        /// <summary>
        /// XML
        /// </summary>
        XML = 4,

        /// <summary>
        /// 字节数组
        /// </summary>
        BYTES = 5,

        /// <summary>
        /// 其它
        /// </summary>
        OTHER = 255,
    }
}
