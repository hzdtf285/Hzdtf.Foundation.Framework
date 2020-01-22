using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 流程关卡服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFlowCensorshipService
    {
        /// <summary>
        /// 根据流程ID查询流程关卡列表
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<FlowCensorshipInfo>> QueryByFlowId(int flowId, string connectionId = null);
    }
}
