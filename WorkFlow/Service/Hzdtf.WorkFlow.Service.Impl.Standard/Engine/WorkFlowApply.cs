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
    /// 工作流申请
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowApply : WorkflowFormBase, IWorkflowApply
    {
        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        /// <param name="currUser">当前用户</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowInitInfo<PersonTimeInfo>> flowIn, FlowCensorshipInInfo findFlowCensorshipIn, BasicUserInfo currUser = null)
        {
            base.AppendSetFindFlowCensorshipIn(flowIn, findFlowCensorshipIn, currUser);
            findFlowCensorshipIn.ActionType = ActionType.SEND;
        }
    }
}
