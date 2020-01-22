using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 表单服务
    /// @黄振东
    /// </summary>
    /// <typeparam name="ConcreteFormInfT">具体表单信息类型</typeparam>
    public partial interface IFormService<ConcreteFormInfT>
    {
        /// <summary>
        /// 设置表单
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="formInfo">表单信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Set(ConcreteFormInfT formInfo, string connectionId = null);

        /// <summary>
        /// 根据流程ID修改流程状态
        /// </summary>
        /// <param name="formInfo">表单信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyFlowStatusByWorkflowId(ConcreteFormInfT formInfo, string connectionId = null);

        /// <summary>
        /// 根据工作流ID移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveByWorkflowId(int workflowId, string connectionId = null);
    }
}
