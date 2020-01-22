using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 流程关卡持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFlowCensorshipPersistence
    {
        /// <summary>
        /// 根据流程ID查询流程关卡列表
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>流程关卡列表</returns>
        IList<FlowCensorshipInfo> SelectByFlowId(int flowId, string connectionId = null);
    }
}
