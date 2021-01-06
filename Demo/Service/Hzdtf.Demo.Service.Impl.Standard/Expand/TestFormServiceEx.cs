using Hzdtf.Demo.Model.Standard;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Demo.Service.Impl.Standard
{
    /// <summary>
    /// 测试表单服务
    /// @ 黄振东
    /// </summary>
    public partial class TestFormService : IFormDataReader<TestFormInfo>, IFormDataReader, IFormService<TestFormInfo>
    {
        /// <summary>
        /// 根据流程ID修改流程状态
        /// </summary>
        /// <param name="testForm">测试表单</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> ModifyFlowStatusByWorkflowId(TestFormInfo testForm, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                return Persistence.UpdateFlowStatusByWorkflowId(testForm, connId) > 0;
            }, null, connectionId);
        }

        /// <summary>
        /// 根据工作流ID读取表单数据
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<TestFormInfo> IFormDataReader<TestFormInfo>.ReaderByWorkflowId([DisplayName2("工作流ID"), Id] int workflowId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<TestFormInfo>((reInfo, connId) =>
            {
                return Persistence.SelectByWorkflowId(workflowId, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 根据工作流ID读取表单数据
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<ConcreteFormInfo> IFormDataReader<ConcreteFormInfo>.ReaderByWorkflowId([DisplayName2("工作流ID"), Id] int workflowId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<ConcreteFormInfo>((reInfo, connId) =>
            {
                return Persistence.SelectByWorkflowId(workflowId, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 根据工作流ID移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> RemoveByWorkflowId([DisplayName2("工作流ID"), Id] int workflowId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                return Persistence.DeleteByWorkflowId(workflowId, connId) > 0;
            }, null, connectionId);
        }
    }
}
