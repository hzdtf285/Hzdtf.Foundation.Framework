﻿using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.Service.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 工作流服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowService : IService<int, WorkflowInfo>
    {
    }
}
