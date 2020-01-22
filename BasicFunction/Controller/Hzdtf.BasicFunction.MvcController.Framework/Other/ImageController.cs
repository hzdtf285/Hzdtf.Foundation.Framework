using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hzdtf.BasicFunction.MvcController.Framework.Other
{
    /// <summary>
    /// 图片控制器
    /// @ 黄振东
    /// </summary>
    public class ImageController : System.Web.Mvc.Controller
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        [HttpGet]
        public void BuilderCheckCode()
        {
            string checkCode = NumberUtil.Random();
            MemoryStream memoryStream = ImageUtil.CreateCodeImg(checkCode);
            Session["VerificationCode"] = checkCode;
            
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(memoryStream.ToArray());
        }
    }
}
