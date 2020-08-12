using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Diversion
{
    /// <summary>
    /// 查找流程关卡接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFindFlowCensorship
    {
        /// <summary>
        /// 查找下一个处理信息
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<FlowCensorshipOutInfo> NextHandler(FlowCensorshipInInfo findFlowCensorshipIn, string connectionId = null, BasicUserInfo currUser = null);
    }
}
