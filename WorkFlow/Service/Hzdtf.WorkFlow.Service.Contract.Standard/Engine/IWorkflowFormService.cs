using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine
{
    /// <summary>
    /// 工作流表单服务接口
    /// @ 黄振东
    /// </summary>
    public interface IWorkflowFormService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowBasicInfo> Save<FormT>(FlowInitInfo<FormT> flowInit, BasicUserInfo currUser = null) where FormT : PersonTimeInfo;

        /// <summary>
        /// 申请
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowBasicInfo> Apply<FormT>(FlowInitInfo<FormT> flowInit, BasicUserInfo currUser = null) where FormT : PersonTimeInfo;
    }
}
