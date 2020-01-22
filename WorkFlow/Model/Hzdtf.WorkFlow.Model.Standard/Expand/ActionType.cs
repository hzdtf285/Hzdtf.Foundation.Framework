using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 动作类型
    /// </summary>
    public enum ActionType : byte
    {
        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")]
        SAVE = 0,

        /// <summary>
        /// 送件
        /// </summary>
        [Description("送件")]
        SEND = 1,

        /// <summary>
        /// 退件
        /// </summary>
        [Description("退件")]
        RETURN = 2,

        /// <summary>
        /// 撤消
        /// </summary>
        [Description("撤消")]
        UNDO = 3,
    }
}
