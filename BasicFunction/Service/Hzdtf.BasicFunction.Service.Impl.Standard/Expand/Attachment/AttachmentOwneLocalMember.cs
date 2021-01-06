using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件归属本地内存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class AttachmentOwnerLocalMember : SingleTypeLocalMemoryBase<short, AttachmentOwnerInfo>, IAttachmentOwnerReader
    {
        #region 属性与字段

        /// <summary>
        /// 字典缓存
        /// </summary>
        private static readonly IDictionary<short, AttachmentOwnerInfo> dicCache = new Dictionary<short, AttachmentOwnerInfo>(1);

        /// <summary>
        /// 同步字典缓存
        /// </summary>
        private static readonly object syncDicCache = new object();

        /// <summary>
        /// 原生附件归属读取
        /// </summary>
        public IAttachmentOwnerReader ProtoAttachmentOwnerReader
        {
            get;
            set;
        }

        #endregion

        #region IAttachmentOwnerReader 接口

        /// <summary>
        /// 根据归属类型读取附件归属信息
        /// </summary>
        /// <param name="type">归属类型</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>附件归属信息</returns>
        public AttachmentOwnerInfo ReaderByOwnerType(short type, BasicUserInfo<int> currUser = null)
        {
            if (dicCache.ContainsKey(type))
            {
                return dicCache[type];
            }

            AttachmentOwnerInfo AttachmentOwnerInfo = ProtoAttachmentOwnerReader.ReaderByOwnerType(type, currUser);
            if (AttachmentOwnerInfo == null)
            {
                return null;
            }

            Add(type, AttachmentOwnerInfo);

            return AttachmentOwnerInfo;
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<short, AttachmentOwnerInfo> GetCache() => dicCache;

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        #endregion
    }
}
