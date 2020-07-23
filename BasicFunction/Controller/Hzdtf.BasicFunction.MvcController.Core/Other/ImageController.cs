using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Hzdtf.BasicFunction.MvcController.Core.Other
{
    /// <summary>
    /// 图片控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        [HttpGet("BuilderCheckCode")]
        public FileContentResult BuilderCheckCode()
        {
            string checkCode = NumberUtil.Random();
            var imageBytes = ImageUtil.CreateCrossPlatformCodeImg(checkCode);
            HttpContext.Session.SetString("VerificationCode", checkCode);

            return File(imageBytes, "image/png");
        }
    }
}
