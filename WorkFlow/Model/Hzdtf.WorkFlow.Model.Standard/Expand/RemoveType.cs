using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 移除类型
    /// @ 黄振东
    /// </summary>
    public enum RemoveType : byte
    {
        /// <summary>
        /// 移除
        /// </summary>
        [Description("移除")]
        REMOVE = 1,

        /// <summary>
        /// 强制移除
        /// </summary>
        [Description("强制移除")]
        FORCE_REMOVE = 2,

        /// <summary>
        /// 撤消
        /// </summary>
        [Description("撤消")]
        UNDO = 3,
    }
}
