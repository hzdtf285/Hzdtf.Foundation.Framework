using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;
using System.Net.Http;

namespace Hzdtf.BasicController.Framework
{
    /// <summary>
    /// JqGrid分页解析筛选
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JqGridPagingParseFilter : IPagingParseFilter
    {
        /// <summary>
        /// 从HTTP请求对象转换为筛选对象
        /// </summary>
        /// <typeparam name="PagingFilterObjectT">分页筛选对象类型</typeparam>
        /// <param name="httpRequest">HTTP请求对象</param>
        /// <param name="pageIndex">页码（从0开始）</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页筛选对象</returns>
        public PagingFilterObjectT ToFilterObjectFromHttp<PagingFilterObjectT>(HttpRequestMessage httpRequest, out int pageIndex, out int pageSize)
        where PagingFilterObjectT : FilterInfo
        {
            pageIndex = pageSize = 0;
            IDictionary<string, string> dicParams = httpRequest.RequestUri.AbsoluteUri.ToDictionaryFromUrlParams();
            if (dicParams.IsNullOrCount0())
            {
                return null;
            }

            PagingFilterObjectT filter = dicParams.ToObject<PagingFilterObjectT, string>();
            if (dicParams.ContainsKey("page") && !string.IsNullOrWhiteSpace(dicParams["page"]))
            {
                pageIndex = Convert.ToInt32(dicParams["page"]) - 1;
            }
            if (dicParams.ContainsKey("rows") && !string.IsNullOrWhiteSpace(dicParams["rows"]))
            {
                pageSize = Convert.ToInt32(dicParams["rows"]);
            }
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

            return filter;
        }
    }
}
