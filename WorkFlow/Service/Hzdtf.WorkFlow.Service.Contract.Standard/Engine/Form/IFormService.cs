using Hzdtf.Utility.Standard.Model;
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
    /// <typeparam name="ConcreteFormInfoT">具体表单信息类型</typeparam>
    public partial interface IFormService<ConcreteFormInfoT>
    {
        /// <summary>
        /// 设置表单
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="formInfo">表单信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Set(ConcreteFormInfoT formInfo, string connectionId = null, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 根据流程ID修改流程状态
        /// </summary>
        /// <param name="formInfo">表单信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyFlowStatusByWorkflowId(ConcreteFormInfoT formInfo, string connectionId = null, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 根据工作流ID移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveByWorkflowId(int workflowId, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
