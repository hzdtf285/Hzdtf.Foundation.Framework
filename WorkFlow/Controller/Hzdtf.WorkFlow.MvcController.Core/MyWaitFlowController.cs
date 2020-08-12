using Hzdtf.BasicController.Core;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Model.Standard.Expand.Filter;
using Hzdtf.WorkFlow.Service.Contract.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.WorkFlow.MvcController.Core
{
    /// <summary>
    /// 我的待办流程控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("MyWaitFlow")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class MyWaitFlowController : PagingControllerBase<DateRangePageInfo, WorkflowInfo, IWorkflowService, WaitHandleFilterInfo>
    {
        /// <summary>
        /// 工作流处理服务
        /// </summary>
        public IWorkflowHandleService WorkflowHandleService
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流审核
        /// </summary>
        public IWorkflowAudit WorkFlowAudit
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "MyWaitFlow";

        /// <summary>
        /// 从服务里查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <returns>返回信息任务</returns>
        protected override Task<ReturnInfo<PagingInfo<WorkflowInfo>>> QueryPageFromServiceAsync(int pageIndex, int pageSize, WaitHandleFilterInfo filter)
        {
            return Task<ReturnInfo<PagingInfo<WorkflowInfo>>>.Run(() => Service.QueryCurrUserWaitHandlePage(pageIndex, pageSize, filter));
        }

        /// <summary>
        /// 根据处理ID修改为已读
        /// </summary>
        /// <param name="handleId">处理ID</param>
        /// <returns>返回信息</returns>
        [HttpPut("ModifyReadedByHandleId/{handleId}")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual ReturnInfo<bool> ModifyReadedByHandleId(int handleId) => WorkflowHandleService.ModifyToReadedById(handleId);

        /// <summary>
        /// 获取审核明细信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="handleId">处理ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("GetAuditDetail/{workflowId}/{handleId}")]
        [Function(FunCodeDefine.AUDIT)]
        public virtual ReturnInfo<WorkflowInfo> GetAuditDetail(int workflowId, int handleId) => Service.FindAuditDetail(workflowId, handleId);

        /// <summary>
        /// 执行审核
        /// </summary>
        /// <param name="flowAudit">流程审核信息</param>
        /// <returns>返回信息</returns>
        [HttpPost("ExecAudit")]
        [Function(FunCodeDefine.AUDIT)]
        public virtual ReturnInfo<bool> ExecAudit(FlowAuditInfo flowAudit) => WorkFlowAudit.Execute(flowAudit.ToFlowIn());

        /// <summary>
        /// 获取流程明细信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="handleId">处理ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("GetFlowDetail/{workflowId}/{handleId}")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual ReturnInfo<WorkflowInfo> GetFlowDetail(int workflowId, int handleId) => Service.FindWaitDetail(workflowId, handleId);
    }
}
