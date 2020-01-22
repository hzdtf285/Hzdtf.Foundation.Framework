using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流表单服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class WorkflowFormService : IWorkflowFormService
    {
        /// <summary>
        /// 工作流申请
        /// </summary>
        public IWorkflowApply WorkflowApply
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流保存
        /// </summary>
        public IWorkflowSave WorkflowSave
        {
            get;
            set;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowBasicInfo> Save<FormT>(FlowInitInfo<FormT> flowInit)
            where FormT : PersonTimeInfo => Execute(flowInit, WorkflowSave);

        /// <summary>
        /// 申请
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowBasicInfo> Apply<FormT>(FlowInitInfo<FormT> flowInit)
            where FormT : PersonTimeInfo => Execute(flowInit, WorkflowApply);

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="workflowInit">工作流初始</param>
        /// <returns>返回信息</returns>
        private ReturnInfo<WorkflowBasicInfo> Execute<FormT>(FlowInitInfo<FormT> flowInit, IWorkflowForm workflowInit)
            where FormT : PersonTimeInfo
        {
            var returnInfo = new ReturnInfo<WorkflowBasicInfo>();
            if (string.IsNullOrWhiteSpace(flowInit.ApplyNo))
            {
                flowInit.ApplyNo = BuilderApplyNo(flowInit, returnInfo);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }
            }

            var flowInfo = flowInit.ToFlowIn();
            ReturnInfo<bool> reWorkflow = workflowInit.Execute(flowInfo);
            returnInfo.FromBasic(reWorkflow);
            if (reWorkflow.Failure())
            {
                return returnInfo;
            }

            returnInfo.Data = new WorkflowBasicInfo()
            {
                Id = flowInit.Id,
                ApplyNo = flowInit.ApplyNo
            };

            return returnInfo;
        }

        /// <summary>
        /// 生成申请单号
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始化</param>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>申请单号</returns>
        protected virtual string BuilderApplyNo<FormT>(FlowInitInfo<FormT> flowInit, ReturnInfo<WorkflowBasicInfo> returnInfo)
            where FormT : PersonTimeInfo => DateTime.Now.ToLongDateTimeNumString();
    }
}
