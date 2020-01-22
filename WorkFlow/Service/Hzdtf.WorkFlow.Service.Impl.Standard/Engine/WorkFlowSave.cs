using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流保存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowSave : WorkflowFormBase, IWorkflowSave
    {
        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowInitInfo<PersonTimeInfo>> flowIn, FlowCensorshipInInfo findFlowCensorshipIn)
        {
            base.AppendSetFindFlowCensorshipIn(flowIn, findFlowCensorshipIn);
            findFlowCensorshipIn.ActionType = ActionType.SAVE;
        }
    }
}
