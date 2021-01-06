using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 附件服务
    /// @ 黄振东
    /// </summary>
    public partial class AttachmentService
    {
        /// <summary>
        /// 附件上传存储
        /// </summary>
        public IAttachmentStore AttachmentUploadStore
        {
            get;
            set;
        }

        /// <summary>
        /// 附件归属读取
        /// </summary>
        public IAttachmentOwnerReader AttachmentOwnerReader
        {
            get;
            set;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="attachments">附件信息列表</param>
        /// <param name="streams">文件流列表</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [ProcTrackLog(IgnoreParamValues = true)]
        public virtual ReturnInfo<bool> Upload(IList<AttachmentInfo> attachments, IList<Stream> streams, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<bool> re = new ReturnInfo<bool>();
            AttachmentStreamInfo[] attachmentStreams = new AttachmentStreamInfo[attachments.Count];
            for (var i = 0; i < attachments.Count; i++)
            {
                attachments[i].FileSize = Convert.ToSingle(streams[i].Length / 1024.00);
                ValiFile(attachments[i], re);
                if (re.Failure())
                {
                    return re;
                }

                attachmentStreams[i] = new AttachmentStreamInfo()
                {
                    FileName = attachments[i].FileName,
                    Stream = streams[i]
                };
            }
            ReturnInfo<IList<string>> returnInfo = AttachmentUploadStore.Upload(currUser, attachmentStreams);
            if (returnInfo.Failure())
            {
                ReturnInfo<bool> result = new ReturnInfo<bool>();
                result.FromBasic(returnInfo);

                return result;
            }

            for (var i = 0; i < returnInfo.Data.Count; i++)
            {
                attachments[i].FileAddress = returnInfo.Data[i];
                attachments[i].ExpandName = attachments[i].FileName.FileExpandName();
            }

            return Add(attachments, currUser: currUser);
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="attachment">附件信息</param>
        /// <param name="stream">文件流列表</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [ProcTrackLog(IgnoreParamValues = true)]
        public virtual ReturnInfo<bool> Upload([DisplayName2("附件"), Model] AttachmentInfo attachment, Stream stream, BasicUserInfo<int> currUser = null)
        {
            AttachmentStreamInfo attachmentStream = new AttachmentStreamInfo()
            {
                FileName = attachment.FileName,
                Stream = stream
            };
            ReturnInfo<IList<string>> returnInfo = AttachmentUploadStore.Upload(currUser, attachmentStream);
            if (returnInfo.Failure())
            {
                ReturnInfo<bool> result = new ReturnInfo<bool>();
                result.FromBasic(returnInfo);

                return result;
            }

            attachment.FileAddress = returnInfo.Data[0];
            attachment.ExpandName = attachment.FileName.FileExpandName();
            attachment.FileSize = Convert.ToSingle(stream.Length / 1024.00);

            return Add(attachment, currUser: currUser);
        }

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> RemoveById([Id] int id, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<bool> re = new ReturnInfo<bool>();

            // 先取出当前ID的文件地址
            ReturnInfo<AttachmentInfo> returnInfo = Find(id, connectionId, currUser);
            if (returnInfo.Failure() || returnInfo.Data == null)
            {
                re.FromBasic(returnInfo);

                return re;
            }

            re = base.RemoveById(id, connectionId, currUser);
            if (re.Failure())
            {
                return re;
            }

            if (!string.IsNullOrWhiteSpace(returnInfo.Data.FileAddress))
            {
                AttachmentUploadStore.Remove(currUser, returnInfo.Data.FileAddress);
            }

            return re;
        }

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> RemoveByIds([DisplayName2("ID集合"), ArrayNotEmpty] int[] ids, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<bool> re = new ReturnInfo<bool>();

            // 先取出当前ID集合的文件地址
            ReturnInfo<IList<AttachmentInfo>> returnInfo = Find(ids, connectionId, currUser);
            if (returnInfo.Failure() || returnInfo.Data.IsNullOrCount0())
            {
                re.FromBasic(returnInfo);

                return re;
            }

            string[] fileAddress = new string[returnInfo.Data.Count];
            for (var i = 0; i < fileAddress.Length; i++)
            {
                fileAddress[i] = returnInfo.Data[i].FileAddress;
            }

            re = base.RemoveByIds(ids, connectionId, currUser);
            if (re.Failure())
            {
                return re;
            }

            AttachmentUploadStore.Remove(currUser, fileAddress);

            return re;
        }

        /// <summary>
        /// 根据归属查询附件列表
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<AttachmentInfo>> QueryByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFunc<IList<AttachmentInfo>>((reInfo) =>
            {
                return Persistence.SelectByOwner(ownerType, ownerId, blurTitle, connectionId);
            });
        }

        /// <summary>
        /// 根据归属统计附件个数
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<int> CountByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFunc<int>((reInfo) =>
            {
                return Persistence.CountByOwner(ownerType, ownerId, blurTitle, connectionId);
            });
        }

        /// <summary>
        /// 根据归属是否存在附件
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> ExistsByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFunc<bool>((reInfo) =>
            {
                return Persistence.CountByOwner(ownerType, ownerId, blurTitle, connectionId) > 0;
            });
        }

        /// <summary>
        /// 根据归属移除
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> RemoveByOwner(short ownerType, int ownerId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            ReturnInfo<IList<AttachmentInfo>> ownerReturnInfo = QueryByOwner(ownerType, ownerId, connectionId: connectionId, currUser: currUser);
            if (returnInfo.Failure() || ownerReturnInfo.Data.IsNullOrCount0())
            {
                returnInfo.FromBasic(ownerReturnInfo);

                return returnInfo;
            }

            Persistence.DeleteByOwner(ownerType, ownerId, connectionId);

            // 查找所有的文件地址
            string[] fileAddress = new string[ownerReturnInfo.Data.Count];
            for (var i = 0; i < fileAddress.Length; i++)
            {
                fileAddress[i] = ownerReturnInfo.Data[i].FileAddress;
            }

            AttachmentUploadStore.Remove(currUser, fileAddress);

            return returnInfo;
        }

        /// <summary>
        /// 验证文件
        /// </summary>
        /// <param name="attachment">附件</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="currUser">当前用户</param>
        private ReturnInfo<bool> ValiFile(AttachmentInfo attachment, ReturnInfo<bool> returnInfo, BasicUserInfo<int> currUser = null)
        {
            AttachmentOwnerInfo attachmentOwner = AttachmentOwnerReader.ReaderByOwnerType(attachment.OwnerType, currUser);
            if (attachmentOwner == null)
            {
                return returnInfo;
            }

            if (!string.IsNullOrWhiteSpace(attachmentOwner.AllowExpands) && !"*".Equals(attachmentOwner.AllowExpands) && attachmentOwner.AllowExpands.ToLower().Contains(attachment.ExpandName))
            {
                returnInfo.SetFailureMsg($"{attachment.FileName}类型不正确，必须符合{attachmentOwner.AllowExpands}");
                return returnInfo;
            }

            if (attachmentOwner.MaxSize != -1 && attachment.FileSize > attachmentOwner.MaxSize)
            {
                returnInfo.SetFailureMsg($"{attachment.FileName}大小超过了最大数：{attachmentOwner.MaxSize}KB");
                return returnInfo;
            }

            return returnInfo;
        }
    }
}
