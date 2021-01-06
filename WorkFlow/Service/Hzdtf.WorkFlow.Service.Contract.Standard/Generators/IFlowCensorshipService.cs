﻿using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.Service.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 流程关卡服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFlowCensorshipService : IService<int, FlowCensorshipInfo>
    {
    }
}
