using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流撤消
    /// </summary>
    [Inject]
    public class WorkflowUndo : WorkflowRemoveBase, IWorkflowUndo
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflow">工作流</param>
        /// <param name="currUser">当前用户</param>
        protected override void Vali(ReturnInfo<bool> returnInfo, WorkflowInfo workflow, BasicUserInfo currUser = null)
        {
            var user = UserTool.GetCurrUser(currUser);
            if (workflow.CreaterId != user.Id)
            {
                returnInfo.SetFailureMsg("Sorry，您不是此流程的发起者，故不能撤消");

                return;
            }
            switch (workflow.FlowStatus)
            {
                case FlowStatusEnum.DRAFT:
                    returnInfo.SetFailureMsg("Sorry，此流程是草稿状态不能撤消");

                    return;

                case FlowStatusEnum.REVERSED:
                    returnInfo.SetFailureMsg("Sorry，此流程已经撤消，不能重复撤消");

                    return;

                case FlowStatusEnum.AUDIT_PASS:
                    returnInfo.SetFailureMsg("Sorry，此流程已经审核通过，不能撤消");

                    return;

                case FlowStatusEnum.AUDIT_NOPASS:
                    returnInfo.SetFailureMsg("Sorry，此流程已经审核不通过，不能撤消");

                    return;

                case FlowStatusEnum.AUDITING:
                    // 只有所有审核者未读才允许撤消
                    foreach (var h in workflow.Handles)
                    {
                        // 本人处理忽略
                        if (h.HandlerId == user.Id)
                        {
                            continue;
                        }

                        if (h.IsReaded)
                        {
                            returnInfo.SetFailureMsg("Sorry，此流程已经被审核者读过，不能撤消");

                            return;
                        }
                    }

                    return;

                default:
                    returnInfo.SetFailureMsg("Sorry，此流程未知状态，不能撤消");

                    return;
            }
        }

        /// <summary>
        /// 获取移除类型
        /// </summary>
        /// <returns>移除类型</returns>
        protected override RemoveType GetRemoveType() => RemoveType.UNDO;

        /// <summary>
        /// 执行核心
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected override void ExecCore(ReturnInfo<bool> returnInfo, WorkflowInfo workflow, string connectionId = null, BasicUserInfo currUser = null)
        {
            var user = UserTool.GetCurrUser(currUser);
            // 除本人外，所有处理者都删除
            foreach (var h in workflow.Handles)
            {
                if (h.HandlerId == user.Id)
                {
                    workflow.CurrHandlerIds = h.HandlerId.ToString();
                    workflow.CurrHandlers = h.Handler;
                    workflow.CurrConcreteCensorshipIds = h.ConcreteConcreteId.ToString();
                    workflow.CurrConcreteCensorships = h.ConcreteConcrete;
                    workflow.CurrFlowCensorshipIds = h.FlowCensorshipId.ToString();
                    workflow.FlowStatus = FlowStatusEnum.REVERSED;

                    continue;
                }

                WorkflowHandlePersistence.DeleteById(h.Id, connectionId);
            }

            WorkflowPersistence.UpdateFlowStatusAndCensorshipAndHandlerById(workflow, connectionId);
        }
    }
}
