using Hzdtf.Utility.Standard.Model;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine
{
    /// <summary>
    /// 工作流审核接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowAudit : IWorkflowEngine<FlowInInfo<FlowAuditInfo>>
    {
    }
}
