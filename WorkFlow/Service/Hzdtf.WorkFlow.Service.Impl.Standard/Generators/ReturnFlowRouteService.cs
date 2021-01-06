using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard
{
    /// <summary>
    /// 退件流程路线服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ReturnFlowRouteService : ServiceBase<int, ReturnFlowRouteInfo, IReturnFlowRoutePersistence>, IReturnFlowRouteService
    {
    }
}
