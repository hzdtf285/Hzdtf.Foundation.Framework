using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicController.Core
{
    /// <summary>
    /// 分页解析筛选接口
    /// @ 黄振东
    /// </summary>
    public interface IPagingParseFilter
    {
        /// <summary>
        /// 从HTTP请求对象转换为筛选对象
        /// </summary>
        /// <typeparam name="PagingFilterObjectT">分页筛选对象类型</typeparam>
        /// <param name="httpRequest">HTTP请求对象</param>
        /// <param name="pageIndex">页码（从0开始）</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页筛选对象</returns>
        PagingFilterObjectT ToFilterObjectFromHttp<PagingFilterObjectT>(HttpRequest httpRequest, out int pageIndex, out int pageSize)
        where PagingFilterObjectT : FilterInfo;
    }
}
