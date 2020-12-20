﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.AAG.Model.Standard
{
    /// <summary>
    /// 流程状态
    /// @ 黄振东
    /// </summary>
    public enum FlowStatusEnum : byte
    {
﻿        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        DRAFT = 0,

﻿        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        AUDITING = 1,

﻿        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        AUDIT_PASS = 2,

﻿        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        AUDIT_NOPASS = 3,
    }
}
