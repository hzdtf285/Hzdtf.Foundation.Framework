using Hzdtf.Utility.Standard.Model;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine
{
    /// <summary>
    /// 流程表单接口
    /// @ 黄振东
    /// </summary>
    public interface IWorkflowForm : IWorkflowEngine<FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>>>
    {
    }
}
