using Hzdtf.BasicController.Core;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;

namespace Hzdtf.BasicFunction.MvcController.Core
{
    /// <summary>
    /// 用户控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("User")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class UserController : ManageControllerBase<UserPageInfo, UserInfo, IUserService, UserFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "User";
    }
}
