using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine
{
    /// <summary>
    /// 表单数据读取接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFormDataReader
    {
        /// <summary>
        /// 根据工作流ID读取表单数据
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<ConcreteFormInfo> ReaderByWorkflowId(int workflowId, string connectionId = null);
    }
}
