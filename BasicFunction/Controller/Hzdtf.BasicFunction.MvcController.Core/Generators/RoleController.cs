using Hzdtf.BasicController.Core;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Hzdtf.BasicFunction.MvcController.Core
{
    /// <summary>
    /// 角色控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("Role")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class RoleController : ManageControllerBase<PageInfo<int>, RoleInfo, IRoleService, KeywordFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "Role";
    }
}
