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
    /// 标准关卡服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class StandardCensorshipService : ServiceBase<int, StandardCensorshipInfo, IStandardCensorshipPersistence>, IStandardCensorshipService
    {
    }
}
