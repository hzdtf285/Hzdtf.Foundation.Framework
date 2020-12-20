﻿using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.AAG.Model.Standard;

namespace Hzdtf.AAG.Persistence.Contract.Standard
{
    /// <summary>
    /// 账单结算持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IBillSettlePersistence : IPersistence<BillSettleInfo>
    {
    }
}
