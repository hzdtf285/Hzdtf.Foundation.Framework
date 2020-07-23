using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;

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
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 生成验证码
        /// </summary>
        [HttpGet("BuilderCheckCode")]
        public FileContentResult BuilderCheckCode()
        {
            var verificationCodeRule = AppConfig["VerificationCodeRule"];
            string checkCode = "EnNum".Equals(verificationCodeRule) ? NumberUtil.EnNumRandom() : NumberUtil.Random();

            var imageBytes = ImageUtil.CreateCrossPlatformCodeImg(checkCode);
            HttpContext.Session.SetString("VerificationCode", checkCode);

            return File(imageBytes, "image/png");
        }
    }
}
