using Hzdtf.BasicController.Core;
using hzdtd.Model.Standard;
using hzdtd.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Standard.Utils;

namespace hzdtd.MvcController.Core
{
    /// <summary>
    /// 群流水基数控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("GroupSerialBase")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class GroupSerialBaseController : ManageControllerBase<PageInfo, GroupSerialBaseInfo, IGroupSerialBaseService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "GroupSerialBase";
    }
}
