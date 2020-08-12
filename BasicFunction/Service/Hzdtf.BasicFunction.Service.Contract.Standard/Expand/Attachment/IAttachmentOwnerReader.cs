using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件归属读取接口
    /// @ 黄振东
    /// </summary>
    public partial interface IAttachmentOwnerReader
    {
        /// <summary>
        /// 根据归属类型读取附件归属信息
        /// </summary>
        /// <param name="type">归属类型</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>附件归属信息</returns>
        AttachmentOwnerInfo ReaderByOwnerType(short type, BasicUserInfo currUser = null);
    }
}
