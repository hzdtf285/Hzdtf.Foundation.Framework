using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.BasicFunction.Model.Standard.Expand.DataDictionaryItem;

namespace Hzdtf.BasicFunction.MvcController.Core
{
    /// <summary>
    /// 数据字典子项控制器
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemController
    {
        /// <summary>
        /// 数据字典服务
        /// </summary>
        public IDataDictionaryService DataDictionaryService
        {
            get;
            set;
        }

        /// <summary>
        /// 数据字典子项扩展服务
        /// </summary>
        public IDataDictionaryItemExpandService DataDictionaryItemExpandService
        {
            get;
            set;
        }

        /// <summary>
        /// 查询所有的数据字典列表
        /// </summary>
        /// <returns>数据字典列表</returns>
        [HttpGet("DataDictionarys")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual IList<DataDictionaryInfo> DataDictionarys() => DataDictionaryService.Query().Data;

        /// <summary>
        /// 执行分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        [HttpGet("PageExpandList")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual Page1ReturnInfo<DataDictionaryItemExpandInfo> PageExpandList(int page, int rows)
        {
            IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
            dicParams.RemoveKey("page");
            dicParams.RemoveKey("rows");

            DataDictionaryItemExpandFilterInfo filter = null;
            if (!dicParams.IsNullOrCount0())
            {
                filter = dicParams.ToObject<DataDictionaryItemExpandFilterInfo, string>();
                if (dicParams.ContainsKey("sidx") && !string.IsNullOrWhiteSpace(dicParams["sidx"]))
                {
                    filter.SortName = dicParams["sidx"];
                }
                if (dicParams.ContainsKey("sord") && !string.IsNullOrWhiteSpace(dicParams["sord"]))
                {
                    if ("desc".Equals(dicParams["sord"]))
                    {
                        filter.Sort = SortEnum.DESC;
                    }
                }
            }

            return Page1ReturnInfo<DataDictionaryItemExpandInfo>.From(DataDictionaryItemExpandService.QueryPage(page - 1, rows, filter));
        }
    }
}
