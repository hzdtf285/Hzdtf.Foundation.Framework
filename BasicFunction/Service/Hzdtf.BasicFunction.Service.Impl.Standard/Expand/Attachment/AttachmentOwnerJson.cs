using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件归属JSON
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class AttachmentOwnerJson : IAttachmentOwnerReader
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件名
        /// </summary>
        private readonly string jsonFileName;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public AttachmentOwnerJson()
            : this($"{AppContext.BaseDirectory}Config/attachmentOwnerConfig.json")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFileName">json文件名</param>
        public AttachmentOwnerJson(string jsonFileName) => this.jsonFileName = jsonFileName;

        #endregion

        #region IAttachmentOwnerReader 接口

        /// <summary>
        /// 根据归属类型读取附件归属信息
        /// </summary>
        /// <param name="type">归属类型</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>附件归属信息</returns>
        public AttachmentOwnerInfo ReaderByOwnerType(short type, BasicUserInfo currUser = null)
        {
            AttachmentOwnerInfo[] attachmentOwners = JsonUtil.Deserialize<AttachmentOwnerInfo[]>(jsonFileName.ReaderFileContent());
            if (attachmentOwners.IsNullOrLength0())
            {
                return null;
            }

            foreach (var att in attachmentOwners)
            {
                if (att.OwnerType == type)
                {
                    return att;
                }
            }

            return null;
        }

        #endregion
    }
}
