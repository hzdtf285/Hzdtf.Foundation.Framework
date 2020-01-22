using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Enums
{
    /// <summary>
    /// 生命周期类型
    /// @ 黄振东
    /// </summary>
    public enum LifecycleType : byte
    {
        /// <summary>
        /// 每次都生成一个实例
        /// </summary>
        DEPENDENCY = 0,

        /// <summary>
        /// 在某个范围内共享实例
        /// </summary>
        LIFETIME_SCOPE = 1,

        /// <summary>
        /// 在指定标签且在子域范围内共享实例
        /// </summary>
        MATCH_LIFETIME_SCOPE = 2,

        /// <summary>
        /// 每次请求范围内共享实例
        /// </summary>
        REQUEST = 3,

        /// <summary>
        /// 在全局范围内共享实例
        /// </summary>
        SIGNLETON = 4
    }
}
