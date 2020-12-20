using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.AAG.Model.Standard
{
    /// <summary>
    /// 业务类型
    /// @ 黄振东
    /// </summary>
    public enum BusinessTypeEnum : byte
    {
﻿        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        REGISTER = 0,

﻿        /// <summary>
        /// 变更
        /// </summary>
        [Description("变更")]
        CHANGE = 1,

﻿        /// <summary>
        /// 商标
        /// </summary>
        [Description("商标")]
        TRADEMARK = 2,

﻿        /// <summary>
        /// 香港公司
        /// </summary>
        [Description("香港公司")]
        HK_COMPANY = 3,

﻿        /// <summary>
        /// 补账
        /// </summary>
        [Description("补账")]
        SUPPLEMENT_ACCOUNT = 4,

﻿        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")]
        CANCELLATION = 5,

﻿        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        OTHER = 99,
    }
}
