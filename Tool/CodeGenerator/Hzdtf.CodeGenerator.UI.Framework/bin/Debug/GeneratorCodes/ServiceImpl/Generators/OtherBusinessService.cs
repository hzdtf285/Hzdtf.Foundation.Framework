using Hzdtf.AAG.Model.Standard;
using Hzdtf.AAG.Persistence.Contract.Standard;
using Hzdtf.AAG.Service.Contract.Standard;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.AAG.Service.Impl.Standard
{
    /// <summary>
    /// 其他业务服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class OtherBusinessService : ServiceBase<OtherBusinessInfo, IOtherBusinessPersistence>, IOtherBusinessService
    {
    }
}
