using Hzdtf.BasicController.Core;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;

namespace Hzdtf.BasicFunction.MvcController.Core
{
    /// <summary>
    /// 附件控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("Attachment")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class AttachmentController : ManageControllerBase<PageInfo<int>, AttachmentInfo, IAttachmentService, AttachmentFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "Attachment";
    }
}
