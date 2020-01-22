using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流审核
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowAudit : WorkflowEngineBase<FlowInInfo<FlowAuditInfo>>, IWorkflowAudit
    {
        /// <summary>
        /// 验证流程输入
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流定义</returns>
        protected override WorkflowDefineInfo ValiFlowIn(ReturnInfo<bool> returnInfo, FlowInInfo<FlowAuditInfo> flowIn, out WorkflowInfo workflow, string connectionId = null)
        {
            workflow = null;
            if (flowIn == null)
            {
                returnInfo.SetFailureMsg("流程输入不能为null");
                return null;
            }
            if (flowIn.Flow == null)
            {
                returnInfo.SetFailureMsg("流程不能为null");
                return null;
            }            

            ReturnInfo<WorkflowHandleInfo> reHandle = WorkflowHandle.Find(flowIn.Flow.HandleId, connectionId);
            if (reHandle.Failure())
            {
                returnInfo.FromBasic(reHandle);
                return null;
            }
            if (reHandle.Data == null)
            {
                returnInfo.SetFailureMsg("找不到工作流处理信息");
                return null;
            }
            if (reHandle.Data.HandlerId != UserTool.CurrUser.Id)
            {
                returnInfo.SetFailureMsg("此处理流程不属于您审核");
                return null;
            }
            if (reHandle.Data.HandleType == HandleTypeEnum.NOTIFY)
            {
                returnInfo.SetFailureMsg("您的处理流程是通知类型，不能审核");
                return null;
            }
            if (reHandle.Data.HandleStatus == HandleStatusEnum.SENDED || reHandle.Data.HandleStatus == HandleStatusEnum.RETURNED)
            {
                returnInfo.SetFailureMsg("您的处理流程是已审核过，不能重复审核");
                return null;
            }
            if (reHandle.Data.HandleStatus == HandleStatusEnum.EFFICACYED)
            {
                returnInfo.SetFailureMsg("您的处理流程已失效，不能审核");
                return null;
            }

            ReturnInfo<WorkflowInfo> reWorkflow = WorkflowService.Find(reHandle.Data.WorkflowId);
            if (reWorkflow.Failure())
            {
                returnInfo.FromBasic(reWorkflow);
                return null;
            }
            if (reWorkflow.Data == null)
            {
                returnInfo.SetFailureMsg("找不到工作流信息");
                return null;
            }
            if (reWorkflow.Data.FlowStatus == FlowStatusEnum.DRAFT)
            {
                returnInfo.SetFailureMsg("此工作流是草稿状态不能审核");
                return null;
            }
            if (reWorkflow.Data.FlowStatus == FlowStatusEnum.AUDIT_NOPASS || reWorkflow.Data.FlowStatus == FlowStatusEnum.AUDIT_PASS)
            {
                returnInfo.SetFailureMsg("此工作流已审核结束");
                return null;
            }

            reHandle.Data.Workflow = reWorkflow.Data;
            flowIn.Flow.WorkflowHandle = reHandle.Data;

            ReturnInfo<WorkflowDefineInfo> reWorkFlowConfig = WorkflowConfigReader.ReaderAllConfig(reWorkflow.Data.WorkflowDefineId, connectionId);
            if (reWorkFlowConfig.Failure())
            {
                returnInfo.FromBasic(reWorkFlowConfig);
                return null;
            }

            return reWorkFlowConfig.Data;
        }

        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowAuditInfo> flowIn, FlowCensorshipInInfo findFlowCensorshipIn)
        {
            findFlowCensorshipIn.ActionType = flowIn.Flow.ActionType;
            findFlowCensorshipIn.Idea = flowIn.Flow.Idea;
            findFlowCensorshipIn.CurrWorkflowHandle = flowIn.Flow.WorkflowHandle;
            findFlowCensorshipIn.Workflow = flowIn.Flow.WorkflowHandle.Workflow;
        }

        /// <summary>
        /// 执行核心
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipOut">查找流程关卡输出</param>
        /// <param name="connectionId">连接ID</param>
        protected override void ExecCore(ReturnInfo<bool> returnInfo, FlowInInfo<FlowAuditInfo> flowIn,
            FlowCensorshipOutInfo findFlowCensorshipOut, string connectionId = null)
        {
            // 更新工作流状态
            WorkflowPersistence.UpdateFlowStatusAndCensorshipAndHandlerById(findFlowCensorshipOut.Workflow, connectionId);

            // 更新当前工作流处理状态
            WorkflowHandlePersistence.UpdateHandleStatusById(findFlowCensorshipOut.CurrConcreteCensorship.WorkflowHandles[0], connectionId);

            string actionStr = flowIn.Flow.ActionType == ActionType.SEND ? "送件" : "退件";
            if (findFlowCensorshipOut.NextConcreteCensorshipHandles.IsNullOrLength0())
            {
                returnInfo.SetSuccessMsg($"申请单号[{findFlowCensorshipOut.Workflow.ApplyNo}]已{actionStr}，等待其他人处理");
                return;
            }
            else
            {
                WorkflowHandleInfo updateEf = new WorkflowHandleInfo()
                {
                    Id = flowIn.Flow.HandleId,
                    FlowCensorshipId = flowIn.Flow.WorkflowHandle.FlowCensorshipId,
                    ConcreteConcreteId = flowIn.Flow.WorkflowHandle.ConcreteConcreteId,
                    WorkflowId = flowIn.Flow.WorkflowHandle.WorkflowId
                };
                updateEf.SetModifyInfo();

                if (flowIn.Flow.ActionType == ActionType.SEND)
                {
                    WorkflowHandlePersistence.UpdateEfficacyedNotIdByWorkflowIdAndFlowCensorshipId(updateEf, connectionId);
                }
                else
                {
                    WorkflowHandlePersistence.UpdateEfficacyedNotIdByWorkflowId(updateEf, connectionId);
                }

                // 插入下一个处理者
                IList<WorkflowHandleInfo> handlers = new List<WorkflowHandleInfo>();
                foreach (var c in findFlowCensorshipOut.NextConcreteCensorshipHandles)
                {
                    foreach (var h in c.WorkflowHandles)
                    {
                        h.WorkflowId = findFlowCensorshipOut.Workflow.Id;
                        handlers.Add(h);
                    }
                }

                ReturnInfo<bool> reHandle = WorkflowHandle.Add(handlers, connectionId);
                if (reHandle.Failure())
                {
                    returnInfo.FromBasic(reHandle);
                    return;
                }
            }

            StringBuilder msg = new StringBuilder($"申请单号[{findFlowCensorshipOut.Workflow.ApplyNo}]");
            if (findFlowCensorshipOut.ActionType == ActionType.SEND)
            {
                msg.AppendFormat("已送到[{0}]", findFlowCensorshipOut.Workflow.CurrHandlers);
                if (findFlowCensorshipOut.IsNextEndCensorship())
                {
                    msg.Append(",审核通过,流程结束");
                }
                else
                {
                    msg.Append("审核");
                }
            }
            else
            {
                msg.AppendFormat("已退给[{0}]", findFlowCensorshipOut.Workflow.CurrHandlers);
                if (findFlowCensorshipOut.IsNextApplicantCensorship())
                {
                    msg.Append(",审核驳回,流程结束");
                }
                else
                {
                    msg.Append("审核");
                }
            }

            returnInfo.SetMsg(msg.ToString());
        }
    }
}
