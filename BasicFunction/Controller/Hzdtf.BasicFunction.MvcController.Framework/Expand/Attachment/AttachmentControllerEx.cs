using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.MvcController.Framework
{
    /// <summary>
    /// 附件控制器
    /// @ 黄振东
    /// </summary>
    public partial class AttachmentController
    {
        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPost]
        [Disabled]
        [Function(FunCodeDefine.ADD_CODE)]
        public override ReturnInfo<bool> Post(AttachmentInfo model)
        {
            return base.Post(model);
        }

        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPut]
        [Disabled]
        [Function(FunCodeDefine.EDIT_CODE)]
        public override ReturnInfo<bool> Put(int id, AttachmentInfo model)
        {
            return base.Put(id, model);
        }

        /// <summary>
        /// 根据ID查找附件
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        public override ReturnInfo<AttachmentInfo> Get(int id)
        {
            ReturnInfo<AttachmentInfo> returnInfo = Service.Find(id);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }
            ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(returnInfo.Data);
            if (reInfo.Failure())
            {
                returnInfo.FromBasic(reInfo);
            }

            return returnInfo;
        }



        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual Page1ReturnInfo<AttachmentInfo> Page(int page, int rows)
        {
            Page1ReturnInfo<AttachmentInfo> page1ReturnInfo = ExecPage(page, rows);
            if (page1ReturnInfo.Failure())
            {
                return page1ReturnInfo;
            }

            ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(page1ReturnInfo.Rows);
            if (reInfo.Failure())
            {
                page1ReturnInfo.FromBasic(reInfo);
            }

            return page1ReturnInfo;
        }

        /// <summary>
        /// 根据附件归属条件获取附件列表
        /// </summary>
        /// <returns>返回信息</returns>
        [Function(FunCodeDefine.QUERY_CODE)]
        [Route("List")]
        public virtual ReturnInfo<IList<AttachmentInfo>> List()
        {
            IDictionary<string, string> dicParams = Request.RequestUri.AbsoluteUri.ToDictionaryFromUrlParams();
            ReturnInfo<IList<AttachmentInfo>> returnInfo = Service.QueryByOwner(Convert.ToInt16(dicParams.GetValue("ownerType")), Convert.ToInt32(dicParams.GetValue("ownerId")), dicParams.GetValue("blurTitle"));
            if (returnInfo.Success())
            {
                ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(returnInfo.Data);
                if (reInfo.Failure())
                {
                    returnInfo.FromBasic(reInfo);
                }
            }

            return returnInfo;
        }

        /// <summary>
        /// 根据归属移除附件
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete()]
        [Function(FunCodeDefine.REMOVE_CODE)]
        [Route("DeleteByOwner/{ownerType}/{ownerId}")]
        public virtual ReturnInfo<bool> DeleteByOwner(short ownerType, int ownerId) => Service.RemoveByOwner(ownerType, ownerId);

        /// <summary>
        /// 过滤下载权限的文件地址
        /// </summary>
        /// <param name="attachments">附件列表</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<bool> FilterDownLoadPermissionFileAddress(IList<AttachmentInfo> attachments)
        {
            if (attachments.IsNullOrCount0())
            {
                return new ReturnInfo<bool>();
            }

            ReturnInfo<bool> returnInfo = UserService.IsCurrUserPermission(MenuCode(), FunCodeDefine.DOWNLOAD_CODE);
            if (returnInfo.Code == ErrCodeDefine.NOT_PERMISSION)
            {
                foreach (var a in attachments)
                {
                    a.FileAddress = null;
                }
                returnInfo.SetSuccessMsg("操作成功");
            }

            return returnInfo;
        }

        /// <summary>
        /// 过滤下载权限的文件地址
        /// </summary>
        /// <param name="attachment">附件</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<bool> FilterDownLoadPermissionFileAddress(AttachmentInfo attachment)
        {
            if (attachment == null)
            {
                return new ReturnInfo<bool>();
            }

            ReturnInfo<bool> returnInfo = UserService.IsCurrUserPermission(MenuCode(), FunCodeDefine.DOWNLOAD_CODE);
            if (returnInfo.Code == ErrCodeDefine.NOT_PERMISSION)
            {
                attachment.FileAddress = null;
                returnInfo.SetSuccessMsg("操作成功");
            }

            return returnInfo;
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected virtual string MenuCode() => "Attachment";
    }
}