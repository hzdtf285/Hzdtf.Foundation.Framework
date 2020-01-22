using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 处理类型
    /// @ 黄振东
    /// </summary>
    public enum HandleTypeEnum : byte
    {
﻿        /// <summary>
        /// 通知
        /// </summary>
        [Description("通知")]
        NOTIFY = 0,

﻿        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        AUDIT = 1,

﻿        /// <summary>
        /// 申请
        /// </summary>
        [Description("申请")]
        APPLY = 2,
    }
}
