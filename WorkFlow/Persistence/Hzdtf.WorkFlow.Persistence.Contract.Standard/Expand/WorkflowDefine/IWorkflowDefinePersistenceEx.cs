using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{ 
    /// <summary>
    /// 工作流定义持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowDefinePersistence
    {
        /// <summary>
        /// 根据编码查询工作流定义信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流定义信息</returns>
        WorkflowDefineInfo SelectByCode(string code, string connectionId = null);
    }
}
