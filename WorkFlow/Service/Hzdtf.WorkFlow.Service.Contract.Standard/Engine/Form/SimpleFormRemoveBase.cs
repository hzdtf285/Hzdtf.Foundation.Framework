using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 简单表单移除基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConcreteFormInfoT">具体表单信息类型</typeparam>
    public abstract class SimpleFormRemoveBase<ConcreteFormInfoT> : BasicFormRemoveBase
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
        /// 执行流程后
        /// </summary>
        /// <param name="workflow">工作流</param>
        /// <param name="removeType">移除类型</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> AfterExecFlow(WorkflowInfo workflow, RemoveType removeType, bool isSuccess, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            if (isSuccess)
            {
                switch (removeType)
                {
                    case RemoveType.REMOVE:
                    case RemoveType.FORCE_REMOVE:

                        return FormService.RemoveByWorkflowId(workflow.Id, connectionId, currUser);

                    case RemoveType.UNDO:
                        ConcreteFormInfoT form = typeof(ConcreteFormInfoT).CreateInstance<ConcreteFormInfoT>();
                        form.WorkflowId = workflow.Id;
                        form.FlowStatus = FlowStatusEnum.REVERSED;
                        form.SetModifyInfo(currUser);

                        return FormService.ModifyFlowStatusByWorkflowId(form, connectionId, currUser);
                }
            }

            return new ReturnInfo<bool>();
        }
    }
}
