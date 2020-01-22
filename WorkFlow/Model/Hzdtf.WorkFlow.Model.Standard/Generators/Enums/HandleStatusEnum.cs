using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 处理状态
    /// @ 黄振东
    /// </summary>
    public enum HandleStatusEnum : byte
    {
﻿        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        UN_HANDLE = 0,

﻿        /// <summary>
        /// 已送件
        /// </summary>
        [Description("已送件")]
        SENDED = 1,

﻿        /// <summary>
        /// 已退件
        /// </summary>
        [Description("已退件")]
        RETURNED = 2,

﻿        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        EFFICACYED = 3,
    }
}
