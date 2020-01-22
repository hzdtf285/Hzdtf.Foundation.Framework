using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.WorkFlow.Model.Standard;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 流程关卡持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFlowCensorshipPersistence : IPersistence<FlowCensorshipInfo>
    {
    }
}
