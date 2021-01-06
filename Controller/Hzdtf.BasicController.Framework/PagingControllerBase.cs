using Hzdtf.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;
using System.Web.Http;

namespace Hzdtf.BasicController.Framework
{
    /// <summary>
    /// 分页控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="PageInfoT">页面信息类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="ServiceT">服务类型</typeparam>
    /// <typeparam name="PageFilterT">分页筛选类型</typeparam>
    public abstract class PagingControllerBase<PageInfoT, ModelT, ServiceT, PageFilterT> : PageControllerBase<PageInfoT>
        where PageInfoT : PageInfo<int>
        where ModelT : SimpleInfo<int>
        where ServiceT : IService<int, ModelT>
        where PageFilterT : FilterInfo
    {
        /// <summary>
        /// 服务
        /// </summary>
        public ServiceT Service
        {
            get;
            set;
        }

        /// <summary>
        /// 执行分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        protected Page1ReturnInfo<ModelT> ExecPage(int page, int rows)
        {
            IDictionary<string, string> dicParams = Request.RequestUri.AbsoluteUri.ToDictionaryFromUrlParams();
            dicParams.RemoveKey("page");
            dicParams.RemoveKey("rows");

            PageFilterT filter = null;
            if (!dicParams.IsNullOrCount0())
            {
                filter = dicParams.ToObject<PageFilterT, string>();
            }

            return Page1ReturnInfo<ModelT>.From(Service.QueryPage(page - 1, rows, filter));
        }
    }
}
