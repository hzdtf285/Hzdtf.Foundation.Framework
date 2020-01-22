using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.WorkFlow.Model.Standard;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 简单表单引擎
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConcreteFormInfoT">具体表单信息类型</typeparam>
    public abstract class SimpleFormEngineBase<ConcreteFormInfoT> : BasicFormEngineBase
        where ConcreteFormInfoT : ConcreteFormInfo
    {
        /// <summary>
        /// 表单服务
        /// </summary>
        public IFormService<ConcreteFormInfoT> FormService
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据读取工厂
        /// </summary>
        public IFormDataReaderFactory FormDataReaderFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 执行流程后
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public override ReturnInfo<bool> AfterExecFlow(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, bool isSuccess, string connectionId = null)
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            if (isSuccess)
            {
                // 当前为申请者关卡
                if (flowCensorshipOut.IsCurrApplicantCensorship() 
                    && (flowCensorshipOut.ActionType == ActionType.SAVE || flowCensorshipOut.ActionType == ActionType.SEND))
                {
                    FlowInInfo<FlowInitInfo<PersonTimeInfo>> conFlowIn;
                    ConcreteFormInfoT form = ToApplyConcreteFormInfo(flowCensorshipOut, flowIn, returnInfo, out conFlowIn);
                    if (returnInfo.Failure())
                    {
                        return returnInfo;
                    }
                    
                    ReturnInfo<ConcreteFormInfo> reFormInfo = FormDataReaderFactory.Create(conFlowIn.Flow.WorkflowCode).ReaderByWorkflowId(flowCensorshipOut.Workflow.Id, connectionId);
                    if (reFormInfo.Failure())
                    {
                        returnInfo.FromBasic(reFormInfo);

                        return returnInfo;
                    }
                    if (reFormInfo.Data != null)
                    {                        
                        switch (reFormInfo.Data.FlowStatus)
                        {
                            case FlowStatusEnum.AUDITING:
                                returnInfo.SetFailureMsg("此表单在审核中不允许重复申请");

                                return returnInfo;

                            case FlowStatusEnum.AUDIT_PASS:
                                returnInfo.SetFailureMsg("此表单已审核通过不允许重复申请");

                                return returnInfo;

                            case FlowStatusEnum.AUDIT_NOPASS:
                                returnInfo.SetFailureMsg("此表单已审核驳回不允许重复申请");

                                return returnInfo;
                        }

                        form.SetModifyInfo();
                    }
                    else
                    {
                        form.SetCreateInfo();
                    }

                    form.ApplyNo = flowCensorshipOut.Workflow.ApplyNo;
                    form.WorkflowId = flowCensorshipOut.Workflow.Id;
                    form.FlowStatus = flowCensorshipOut.Workflow.FlowStatus;

                    returnInfo = FormService.Set(form, connectionId);
                } // 下一关卡如果是结束关卡（送件）或是申请关卡（退件）
                else if ((flowCensorshipOut.IsNextEndCensorship() && flowCensorshipOut.ActionType == ActionType.SEND)
                    || (flowCensorshipOut.IsNextApplicantCensorship() && flowCensorshipOut.ActionType == ActionType.RETURN))
                {
                    ConcreteFormInfoT form = typeof(ConcreteFormInfoT).CreateInstance<ConcreteFormInfoT>();
                    form.WorkflowId = flowCensorshipOut.Workflow.Id;
                    form.FlowStatus = flowCensorshipOut.Workflow.FlowStatus;
                    form.SetModifyInfo();

                    returnInfo = FormService.ModifyFlowStatusByWorkflowId(form, connectionId);
                }
            }

            return returnInfo;
        }

        /// <summary>
        /// 转换为申请的具体表单信息
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="conFlowIn">具体流程输入</param>
        /// <returns>具体表单信息</returns>
        private ConcreteFormInfoT ToApplyConcreteFormInfo(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, ReturnInfo<bool> returnInfo, out FlowInInfo<FlowInitInfo<PersonTimeInfo>> conFlowIn)
        {
            conFlowIn = flowIn as FlowInInfo<FlowInitInfo<PersonTimeInfo>>;
            if (conFlowIn == null)
            {
                returnInfo.SetFailureMsg("流程输入不能转换为FlowInInfo<FlowApplyInfo<PersonTimeInfo>>");
                return null;
            }
            ConcreteFormInfoT form = conFlowIn.Form as ConcreteFormInfoT;
            if (form == null)
            {
                returnInfo.SetFailureMsg($"表单不能转换为{typeof(ConcreteFormInfoT).Name}");
                return null;
            }

            return form;
        }
    }
}
