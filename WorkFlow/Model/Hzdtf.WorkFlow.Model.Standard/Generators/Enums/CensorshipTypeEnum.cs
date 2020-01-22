using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard
{
    /// <summary>
    /// 关卡类型
    /// @ 黄振东
    /// </summary>
    public enum CensorshipTypeEnum : byte
    {
﻿        /// <summary>
        /// 标准
        /// </summary>
        [Description("标准")]
        STANDARD = 0,

﻿        /// <summary>
        /// 角色
        /// </summary>
        [Description("角色")]
        ROLE = 1,

﻿        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        USER = 2,
    }
}
