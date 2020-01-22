using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.MvcController.Core
{
    /// <summary>
    /// 工作流表单控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FormT">表单类型</typeparam>
    public abstract class WorkflowFormControllerBase<FormT> : ControllerBase
        where FormT : PersonTimeInfo
    {
        /// <summary>
        /// 工作流初始服务
        /// </summary>
        public IWorkflowFormService WorkflowInitService
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流移除
        /// </summary>
        public IWorkflowRemove WorkflowRemove
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流强制移除
        /// </summary>
        public IWorkflowForceRemove WorkflowForceRemove
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流撤消
        /// </summary>
        public IWorkflowUndo WorkflowUndo
        {
            get;
            set;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        [HttpPost("Save")]
        [Function(FunCodeDefine.SAVE_CODE)]
        public virtual ReturnInfo<WorkflowBasicInfo> Save(FlowInitInfo<FormT> flowInit)
        {
            return Execute(flowInit, () =>
            {
                return WorkflowInitService.Save(flowInit);
            });
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        [HttpPost("Apply")]
        [Function(FunCodeDefine.APPLY)]
        public virtual ReturnInfo<WorkflowBasicInfo> Apply(FlowInitInfo<FormT> flowInit)
        {
            return Execute(flowInit, () =>
            {
                return WorkflowInitService.Apply(flowInit);
            });
        }

        /// <summary>
        /// 根据工作流ID移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("RemoveByWorkflowId")]
        [Function(FunCodeDefine.REMOVE_CODE)]
        public virtual ReturnInfo<bool> RemoveByWorkflowId(int workflowId) => WorkflowRemove.Execute(workflowId);

        /// <summary>
        /// 根据工作流ID强制移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("ForceRemoveByWorkflowId")]
        [Function(FunCodeDefine.FORCE_REMOVE)]
        public virtual ReturnInfo<bool> ForceRemoveByWorkflowId(int workflowId) => WorkflowForceRemove.Execute(workflowId);

        /// <summary>
        /// 根据工作流ID撤消
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("UndoByWorkflowId")]
        [Function(FunCodeDefine.UNDO)]
        public virtual ReturnInfo<bool> UndoByWorkflowId(int workflowId) => WorkflowUndo.Execute(workflowId);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <param name="func">回调执行</param>
        /// <returns>返回信息</returns>
        private ReturnInfo<WorkflowBasicInfo> Execute(FlowInitInfo<FormT> flowInit, Func<ReturnInfo<WorkflowBasicInfo>> func)
        {
            if (flowInit == null)
            {
                var returnInfo = new ReturnInfo<string>();

                returnInfo.SetFailureMsg("流程表单数据不能为null");
            }

            flowInit.WorkflowCode = GetWorkflowCode();

            SetFlowInitPropertys(flowInit);

            return func();
        }

        /// <summary>
        /// 获取工作流编码
        /// </summary>
        /// <returns>工作流编码</returns>
        protected abstract string GetWorkflowCode();

        /// <summary>
        /// 设置流程初始化属性
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        protected virtual void SetFlowInitPropertys(FlowInitInfo<FormT> flowInit) { }
    }
}
