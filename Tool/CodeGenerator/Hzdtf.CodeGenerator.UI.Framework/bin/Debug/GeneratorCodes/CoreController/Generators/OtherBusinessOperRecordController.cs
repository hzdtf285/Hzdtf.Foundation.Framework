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
    /// 其他业务_操作记录控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("OtherBusinessOperRecord")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class OtherBusinessOperRecordController : ManageControllerBase<PageInfo, OtherBusinessOperRecordInfo, IOtherBusinessOperRecordService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "OtherBusinessOperRecord";
    }
}
