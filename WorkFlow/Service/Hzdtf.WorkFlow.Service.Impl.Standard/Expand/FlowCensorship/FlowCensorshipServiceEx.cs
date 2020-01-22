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
    /// 流程关卡服务接口
    /// @ 黄振东
    /// </summary>
    public partial class FlowCensorshipService
    {
        /// <summary>
        /// 根据流程ID查询流程关卡列表
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<FlowCensorshipInfo>> QueryByFlowId([DisplayName2("流程ID"), Id] int flowId, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<IList<FlowCensorshipInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectByFlowId(flowId, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }
    }
}
