using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 基本表单移除基类
    /// @ 黄振东
    /// </summary>
    public abstract class BasicFormRemoveBase : IFormRemove
    {
        /// <summary>
        /// 执行流程前
        /// </summary>
        /// <param name="workflow">工作流</param>
        /// <param name="removeType">移除类型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> BeforeExecFlow(WorkflowInfo workflow, RemoveType removeType, string connectionId = null, BasicUserInfo currUser = null)
        {
            return new ReturnInfo<bool>();
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
        public virtual ReturnInfo<bool> AfterExecFlow(WorkflowInfo workflow, RemoveType removeType, bool isSuccess, string connectionId = null, BasicUserInfo currUser = null)
        {
            return new ReturnInfo<bool>();
        }
    }
}
