using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件存储接口
    /// @ 黄振东
    /// </summary>
    public interface IAttachmentStore
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="currUser">当前用户</param>
        /// <param name="attachmentStream">附件流</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<string>> Upload(BasicUserInfo<int> currUser = null, params AttachmentStreamInfo[] attachmentStream);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="currUser">当前用户</param>
        /// <param name="fileAddress">文件地址</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Remove(BasicUserInfo<int> currUser = null, params string[] fileAddress);
    }
}
