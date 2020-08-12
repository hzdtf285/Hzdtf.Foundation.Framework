using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 退件流程路线服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IReturnFlowRouteService : IReturnFlowRouteServiceAsync
    {
        /// <summary>
        /// 根据流程关卡ID查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ReturnFlowRouteInfo>> QueryByFlowCensorshipId(int flowCensorshipId, string connectionId = null, BasicUserInfo currUser = null);

        /// <summary>
        /// 根据流程关卡ID数组查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ReturnFlowRouteInfo>> QueryByFlowCensorshipIds(int[] flowCensorshipIds, string connectionId = null, BasicUserInfo currUser = null);
    }
}
