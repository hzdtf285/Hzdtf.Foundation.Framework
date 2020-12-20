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
    /// 收款统计明细控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("VPayeeTotalDetail")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class VPayeeTotalDetailController : ManageControllerBase<PageInfo, VPayeeTotalDetailInfo, IVPayeeTotalDetailService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "VPayeeTotalDetail";
    }
}
