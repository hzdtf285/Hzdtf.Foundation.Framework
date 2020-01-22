using Hzdtf.BasicController.Core;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand.Filter;
using Hzdtf.WorkFlow.Service.Contract.Standard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.MvcController.Core
{
    /// <summary>
    /// 我申请的流程控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("MyApplyFlow")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class MyApplyFlowController : PagingControllerBase<DateRangePageInfo, WorkflowInfo, IWorkflowService, ApplyFlowFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "MyApplyFlow";

        /// <summary>
        /// 从服务里查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <returns>返回信息</returns>
        protected override ReturnInfo<PagingInfo<WorkflowInfo>> QueryPageFromService(int pageIndex, int pageSize, ApplyFlowFilterInfo filter)
        {
            return Service.QueryCurrUserApplyFlowPage(pageIndex, pageSize, filter);
        }

        /// <summary>
        /// 获取流程明细信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("GetFlowDetail/{workflowId}")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual ReturnInfo<WorkflowInfo> GetFlowDetail(int workflowId) => Service.FindCurrUserApplyDetail(workflowId);
    }
}
