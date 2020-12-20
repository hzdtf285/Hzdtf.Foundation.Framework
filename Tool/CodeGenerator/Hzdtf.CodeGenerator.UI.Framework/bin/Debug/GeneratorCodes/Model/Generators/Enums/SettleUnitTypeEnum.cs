using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.AAG.Model.Standard
{
    /// <summary>
    /// 结算单位类型
    /// @ 黄振东
    /// </summary>
    public enum SettleUnitTypeEnum : byte
    {
﻿        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        YEAR = 0,

﻿        /// <summary>
        /// 季
        /// </summary>
        [Description("季")]
        QUARTER = 1,

﻿        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        MONTH = 2,
    }
}
