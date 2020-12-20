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
    /// 合约客户服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ContractCustomerService : ServiceBase<ContractCustomerInfo, IContractCustomerPersistence>, IContractCustomerService
    {
    }
}
