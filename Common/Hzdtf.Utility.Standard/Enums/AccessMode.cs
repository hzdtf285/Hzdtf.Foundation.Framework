using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Enums
{
    /// <summary>
    /// 访问模式
    /// @ 黄振东
    /// </summary>
    public enum AccessMode : byte
    {
        /// <summary>
        /// 主
        /// </summary>
        MASTER = 1,

        /// <summary>
        /// 从
        /// </summary>
        SLAVE = 2
    }
}
