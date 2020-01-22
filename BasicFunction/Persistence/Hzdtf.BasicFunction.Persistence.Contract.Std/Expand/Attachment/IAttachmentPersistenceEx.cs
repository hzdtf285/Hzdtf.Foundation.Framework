using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 附件持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IAttachmentPersistence
    {
        /// <summary>
        /// 根据归属查询附件列表
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>附件列表</returns>
        IList<AttachmentInfo> SelectByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null);

        /// <summary>
        /// 根据归属查询统计附件个数
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>附件个数</returns>
        int CountByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null);

        /// <summary>
        /// 根据归属删除
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByOwner(short ownerType, int ownerId, string connectionId = null);
    }
}
