using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.AutoMapperExtensions;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hzdtf.BasicFunction.MvcController.Core.Expand.Attachment
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
        /// 上传附件
        /// </summary>
        /// <param name="simpleAttachment">简单附件</param>
        [HttpPost]
        [Function("Upload")]
        public virtual ReturnInfo<bool> Upload(SimpleAttachmentInfo simpleAttachment)
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                returnInfo.SetFailureMsg("请至少上传一个文件");

                return returnInfo;
            }

            var attachment = AutoMapperUtil.Mapper.Map<SimpleAttachmentInfo, AttachmentInfo>(simpleAttachment);
            BeforeUpload(returnInfo, attachment);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            IList<AttachmentInfo> attachments = new List<AttachmentInfo>(Request.Form.Files.Count);
            IList<Stream> streams = new List<Stream>(Request.Form.Files.Count);
            foreach (var file in Request.Form.Files)
            {
                attachment.FileName = file.FileName;
                attachments.Add(attachment.Clone() as AttachmentInfo);
                streams.Add(file.OpenReadStream());
            }

            returnInfo = AttachmentService.Upload(attachments, streams);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            AfterUpload(returnInfo, attachments);           

            return returnInfo;
        }

        /// <summary>
        /// 上传附件前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="attachment">附件</param>
        protected virtual void BeforeUpload(ReturnInfo<bool> returnInfo, AttachmentInfo attachment) { }

        /// <summary>
        /// 上传附件后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="attachments">附件列表</param>
        protected virtual void AfterUpload(ReturnInfo<bool> returnInfo, IList<AttachmentInfo> attachments) { }
    }
}
