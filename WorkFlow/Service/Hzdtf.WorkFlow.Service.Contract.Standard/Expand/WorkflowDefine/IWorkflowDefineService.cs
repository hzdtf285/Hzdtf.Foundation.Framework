using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 工作流定义服务
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowDefineService
    {
        /// <summary>
        /// 根据编码查询工作流定义信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowDefineInfo> FindByCode(string code, string connectionId = null);
    }
}
