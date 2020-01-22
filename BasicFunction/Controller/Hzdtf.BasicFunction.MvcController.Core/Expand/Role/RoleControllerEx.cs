using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.BasicFunction.Model.Standard;
using System.Net;

namespace Hzdtf.BasicFunction.MvcController.Core
{
    /// <summary>
    /// 角色控制器
    /// @ 黄振东
    /// </summary>
    public partial class RoleController
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns>文件内容结果</returns>
        [HttpGet("Export")]
        [Function(FunCodeDefine.EXPORT_EXCEL_CODE)]
        public virtual FileContentResult Export()
        {
            IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
            KeywordFilterInfo filter = dicParams.ToObject<KeywordFilterInfo, string>();
            ReturnInfo<IList<RoleInfo>> returnInfo = Service.QueryByFilter(filter);
            if (returnInfo.Failure())
            {
                return File(new byte[] { 0 }, null);
            }

            Response.Headers.Add("Content-Disposition", "attachment;filename=" + WebUtility.UrlEncode("角色_" + DateTime.Now.ToFixedDate() + ".xls"));

            try
            {
                return File(returnInfo.Data.ToExcelBytes(), "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {
                Log.ErrorAsync("导出Excel发生异常", ex);
                return null;
            }
        }
    }
}
