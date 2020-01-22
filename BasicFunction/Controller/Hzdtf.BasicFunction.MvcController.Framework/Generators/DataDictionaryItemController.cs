using Hzdtf.BasicController.Framework;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.BasicFunction.Model.Standard.Expand.DataDictionaryItem;

namespace Hzdtf.BasicFunction.MvcController.Framework
{
    /// <summary>
    /// 数据字典子项控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("DataDictionary")]
    [RoutePrefix("api/DataDictionaryItem")]
    [System.Web.Mvc.Authorize]
    public partial class DataDictionaryItemController : ManageControllerBase<PageInfo, DataDictionaryItemInfo, IDataDictionaryItemService, DataDictionaryItemFilterInfo>
    {
        /// <summary>
        /// 获取页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        [Route("PageData")]
        public virtual ReturnInfo<PageInfo> PageData() => ExecPageData("DataDictionary");

        /// <summary>
        /// 根据ID集合批量移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>返回信息</returns>
        [HttpDelete]
        [Function(FunCodeDefine.REMOVE_CODE)]
        [Route("BatchRemove")]
        public virtual ReturnInfo<bool> BatchRemove(int[] ids) => Service.RemoveByIds(ids);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual Page1ReturnInfo<DataDictionaryItemInfo> Page(int page, int rows) => ExecPage(page, rows);
    }
}
