using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hzdtf.BasicFunction.MvcController.Framework.Expand.Attachment
{
    /// <summary>
    /// 附件扩展控制器
    /// @ 黄振东
    /// </summary>
    [Authorize]
    [Inject]
    [Menu("Attachment")]
    public class AttachmentExController : Controller
    {
        /// <summary>
        /// 附件服务
        /// </summary>
        public IAttachmentService AttachmentService
        {
            get;
            set;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="attachmentInfo">附件信息</param>
        [System.Web.Http.HttpPost]
        [Function("Upload")]
        public ReturnInfo<bool> Upload(AttachmentInfo attachmentInfo)
        {
            if (Request.Files == null || Request.Files.Count == 0)
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                returnInfo.SetFailureMsg("请至少上传一个文件");

                return returnInfo;
            }

            IList<AttachmentInfo> attachments = new List<AttachmentInfo>(Request.Files.Count);
            IList<Stream> streams = new List<Stream>(Request.Files.Count);
            for (var i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase httpFile = Request.Files[i] as HttpPostedFileBase;
                attachmentInfo.FileName = httpFile.FileName;
                attachments.Add(attachmentInfo.Clone() as AttachmentInfo);
                streams.Add(httpFile.InputStream);
            }

            return AttachmentService.Upload(attachments, streams);
        }
    }
}