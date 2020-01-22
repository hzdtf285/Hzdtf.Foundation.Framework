using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 退件流程路线持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IReturnFlowRoutePersistence
    {
        /// <summary>
        /// 根据流程关卡ID查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>退件流程路线列表</returns>
        IList<ReturnFlowRouteInfo> SelectByFlowCensorshipId(int flowCensorshipId, string connectionId = null);

        /// <summary>
        /// 根据流程关卡ID数组查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>退件流程路线列表</returns>
        IList<ReturnFlowRouteInfo> SelectByFlowCensorshipIds(int[] flowCensorshipIds, string connectionId = null);
    }
}
