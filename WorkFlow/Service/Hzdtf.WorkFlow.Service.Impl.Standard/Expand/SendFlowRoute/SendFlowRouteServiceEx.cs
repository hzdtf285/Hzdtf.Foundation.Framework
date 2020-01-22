using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard
{
    /// <summary>
    /// 送件流程路线服务
    /// @ 黄振东
    /// </summary>
    public partial class SendFlowRouteService
    {
        /// <summary>
        /// 根据流程关卡ID查询送件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<SendFlowRouteInfo>> QueryByFlowCensorshipId([DisplayName2("流程关卡ID"), Id] int flowCensorshipId, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<IList<SendFlowRouteInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectByFlowCensorshipId(flowCensorshipId, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 根据流程关卡ID数组查询送件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<SendFlowRouteInfo>> QueryByFlowCensorshipIds(int[] flowCensorshipIds, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<IList<SendFlowRouteInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectByFlowCensorshipIds(flowCensorshipIds, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }
    }
}
