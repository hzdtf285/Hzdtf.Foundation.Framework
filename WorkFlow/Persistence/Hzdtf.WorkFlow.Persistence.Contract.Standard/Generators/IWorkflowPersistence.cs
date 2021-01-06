using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.WorkFlow.Model.Standard;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 工作流持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowPersistence : IPersistence<int, WorkflowInfo>
    {
    }
}
