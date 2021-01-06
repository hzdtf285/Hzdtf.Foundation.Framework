﻿using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流表单基类
    /// @ 黄振东
    /// </summary>
    public abstract class WorkflowFormBase : WorkflowEngineBase<FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>>>, IWorkflowForm
    {
        #region 重写父类的方法

        /// <summary>
        /// 验证流程输入
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>工作流定义</returns>
        protected override WorkflowDefineInfo ValiFlowIn(ReturnInfo<bool> returnInfo, FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn, out WorkflowInfo workflow, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            workflow = null;
            ValiBasicInParam(returnInfo, flowIn);
            if (returnInfo.Failure())
            {
                return null;
            }

            ReturnInfo<WorkflowDefineInfo> reWorkFlowConfig = WorkflowConfigReader.ReaderAllConfig(flowIn.Flow.WorkflowCode, connectionId, currUser);
            if (reWorkFlowConfig.Failure())
            {
                returnInfo.FromBasic(reWorkFlowConfig);
                return null;
            }

            ValiDbParam(returnInfo, flowIn, out workflow, connectionId, currUser);
            if (returnInfo.Failure())
            {
                return null;
            }

            return reWorkFlowConfig.Data;
        }

        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        /// <param name="currUser">当前用户</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn, FlowCensorshipInInfo findFlowCensorshipIn, BasicUserInfo<int> currUser = null)
        {
            findFlowCensorshipIn.ApplyNo = flowIn.Flow.ApplyNo;
            findFlowCensorshipIn.Title = flowIn.Flow.Title;
            findFlowCensorshipIn.Idea = flowIn.Flow.Idea;
        }

        /// <summary>
        /// 执行核心
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipOut">查找流程关卡输出</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected override void ExecCore(ReturnInfo<bool> returnInfo, FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn,
            FlowCensorshipOutInfo findFlowCensorshipOut, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            // 操作工作流
            ReturnInfo<bool> reWorkflow = WorkflowService.Set(findFlowCensorshipOut.Workflow, connectionId, currUser);
            if (reWorkflow.Failure())
            {
                returnInfo.FromBasic(reWorkflow);
                return;
            }
            if (flowIn.Flow.Id < 1)
            {
                flowIn.Flow.Id = findFlowCensorshipOut.Workflow.Id;
            }

            // 操作工作流处理
            var currHandle = findFlowCensorshipOut.CurrConcreteCensorship.WorkflowHandles[0];
            currHandle.WorkflowId = findFlowCensorshipOut.Workflow.Id;

            var existsHandleReturnInfo = WorkflowHandle.FindByWorkflowIdAndFlowCensorshipIdAndHandlerId(currHandle.WorkflowId, currHandle.FlowCensorshipId, currHandle.HandlerId, connectionId, currUser);
            if (existsHandleReturnInfo.Failure())
            {
                returnInfo.FromBasic(existsHandleReturnInfo);
                return;
            }

            var user = UserTool<int>.GetCurrUser(currUser);
            if (existsHandleReturnInfo.Data != null)
            {
                if (existsHandleReturnInfo.Data.HandlerId != user.Id)
                {
                    returnInfo.SetFailureMsg($"Sorry，此流程不是您处理的，无权限操作");

                    return;
                }

                currHandle.Id = existsHandleReturnInfo.Data.Id;
                currHandle.SetModifyInfo(currUser);            
            }

            ReturnInfo<bool> reHandle = WorkflowHandle.Set(findFlowCensorshipOut.CurrConcreteCensorship.WorkflowHandles[0], connectionId, currUser);
            if (reHandle.Failure())
            {
                returnInfo.FromBasic(reHandle);
                return;
            }

            if (findFlowCensorshipOut.NextConcreteCensorshipHandles.IsNullOrLength0())
            {
                returnInfo.SetMsg($"申请单号[{findFlowCensorshipOut.Workflow.ApplyNo}]已保存为草稿");
                return;
            }

            IList<WorkflowHandleInfo> handlers = new List<WorkflowHandleInfo>();
            foreach (var c in findFlowCensorshipOut.NextConcreteCensorshipHandles)
            {
                foreach (var h in c.WorkflowHandles)
                {
                    h.WorkflowId = findFlowCensorshipOut.Workflow.Id;
                    handlers.Add(h);
                }
            }

            reHandle = WorkflowHandle.Add(handlers, connectionId, currUser);
            if (reHandle.Failure())
            {
                returnInfo.FromBasic(reHandle);
                return;
            }

            returnInfo.SetMsg($"申请单号[{findFlowCensorshipOut.Workflow.ApplyNo}]已送到[{findFlowCensorshipOut.Workflow.CurrHandlers}]审核");
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证基本的输入参数
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        private void ValiBasicInParam(ReturnInfo<bool> returnInfo, FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn)
        {
            if (flowIn == null)
            {
                returnInfo.SetFailureMsg("流程输入不能为null");
                return;
            }
            if (flowIn.Flow == null)
            {
                returnInfo.SetFailureMsg("流程不能为null");
                return;
            }
            if (flowIn.Form == null)
            {
                returnInfo.SetFailureMsg("表单不能为null");
                return;
            }
            if (string.IsNullOrWhiteSpace(flowIn.Flow.WorkflowCode))
            {
                returnInfo.SetFailureMsg("请输入工作流编码");
                return;
            }
            if (flowIn.Flow.Id < 1 && string.IsNullOrWhiteSpace(flowIn.Flow.ApplyNo))
            {
                returnInfo.SetFailureMsg("请输入申请单号");
                return;
            }
            if (string.IsNullOrWhiteSpace(flowIn.Flow.Title))
            {
                returnInfo.SetFailureMsg("请输入标题");
                return;
            }
        }

        /// <summary>
        /// 验证数据库参数
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        private void ValiDbParam(ReturnInfo<bool> returnInfo, FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn, out WorkflowInfo workflow, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            workflow = null;
            ReturnInfo<WorkflowDefineInfo> reWorkFlowConfig = WorkflowConfigReader.ReaderAllConfig(flowIn.Flow.WorkflowCode, connectionId, currUser);
            if (reWorkFlowConfig.Failure())
            {
                returnInfo.FromBasic(reWorkFlowConfig);
                return;
            }

            var user = UserTool<int>.GetCurrUser(currUser);
            if (flowIn.Flow.Id > 0)
            {
                var reInfo = WorkflowService.Find(flowIn.Flow.Id, connectionId, currUser);
                if (reInfo.Failure())
                {
                    returnInfo.FromBasic(reInfo);

                    return;
                }
                if (reInfo.Data == null)
                {
                    returnInfo.SetFailureMsg($"找不到工作流ID[{flowIn.Flow.Id}]的数据");

                    return;
                }

                if (reInfo.Data.CreaterId != user.Id)
                {
                    returnInfo.SetFailureMsg($"Sorry，此流程不是您创建的，无权限操作");

                    return;
                }

                workflow = reInfo.Data;

                flowIn.Flow.Id = workflow.Id;
                flowIn.Flow.ApplyNo = workflow.ApplyNo;
            }
            else
            {
                ReturnInfo<bool> reInfo = WorkflowService.ExistsByApplyNo(flowIn.Flow.ApplyNo, connectionId, currUser);
                if (reInfo.Failure())
                {
                    returnInfo.FromBasic(reInfo);

                    return;
                }
                if (reInfo.Data)
                {
                    returnInfo.SetFailureMsg($"申请单号[{flowIn.Flow.ApplyNo}]已存在");

                    return;
                }
            }
        }

        #endregion
    }
}
