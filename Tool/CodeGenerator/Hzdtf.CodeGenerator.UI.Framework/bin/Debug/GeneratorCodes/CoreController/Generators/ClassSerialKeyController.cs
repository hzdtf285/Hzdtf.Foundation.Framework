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
    /// 班级流水键控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("ClassSerialKey")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class ClassSerialKeyController : ManageControllerBase<PageInfo, ClassSerialKeyInfo, IClassSerialKeyService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "ClassSerialKey";
    }
}
