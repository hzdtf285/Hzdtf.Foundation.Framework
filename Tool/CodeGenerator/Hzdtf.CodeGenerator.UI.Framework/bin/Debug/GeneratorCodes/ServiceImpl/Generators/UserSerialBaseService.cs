using hzdtd.Model.Standard;
using hzdtd.Persistence.Contract.Standard;
using hzdtd.Service.Contract.Standard;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace hzdtd.Service.Impl.Standard
{
    /// <summary>
    /// 用户流水基数服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class UserSerialBaseService : ServiceBase<UserSerialBaseInfo, IUserSerialBasePersistence>, IUserSerialBaseService
    {
    }
}
