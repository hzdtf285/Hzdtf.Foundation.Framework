using Hzdtf.BasicController.Core;
using Hzdtf.AAG.Model.Standard;
using Hzdtf.AAG.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.AAG.MvcController.Core
{
    /// <summary>
    /// 合约客户_收款明细控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("ContractCustomerPayeeDetail")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class ContractCustomerPayeeDetailController : ManageControllerBase<PageInfo, ContractCustomerPayeeDetailInfo, IContractCustomerPayeeDetailService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "ContractCustomerPayeeDetail";
    }
}
