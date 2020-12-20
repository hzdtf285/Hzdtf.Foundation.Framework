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
    /// 其他业务控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("OtherBusiness")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class OtherBusinessController : ManageControllerBase<PageInfo, OtherBusinessInfo, IOtherBusinessService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "OtherBusiness";
    }
}
