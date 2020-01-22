using Hzdtf.BasicController.Core;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Demo.Model.Standard;
using Hzdtf.Demo.Service.Contract.Standard;

namespace Hzdtf.Demo.MvcController.Core
{
    /// <summary>
    /// 测试表单控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("TestForm")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class TestFormController : ManageControllerBase<PageInfo, TestFormInfo, ITestFormService, FilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "TestForm";
    }
}
