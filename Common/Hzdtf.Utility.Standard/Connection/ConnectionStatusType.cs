using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接状态类型
    /// @ 黄振东
    /// </summary>
    public enum ConnectionStatusType : byte
    {
        /// <summary>
        /// 已关闭
        /// </summary>
        CLOSED = 0,

        /// <summary>
        /// 已打开
        /// </summary>
        OPENED = 1
    }
}
