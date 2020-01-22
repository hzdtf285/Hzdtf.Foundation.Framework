using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 工作流持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowPersistence
    {
        /// <summary>
        /// 根据申请单号统计记录数
        /// </summary>
        /// <param name="applyNo">申请单号</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>记录数</returns>
        int CountByApplyNo(string applyNo, string connectionId = null);

        /// <summary>
        /// 查询待办的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<WorkflowInfo> SelectWaitHandlePage(int pageIndex, int pageSize, WaitHandleFilterInfo filter, string connectionId = null);

        /// <summary>
        /// 查询已审核的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<WorkflowInfo> SelectAuditedHandlePage(int pageIndex, int pageSize, AuditFlowFilterInfo filter, string connectionId = null);

        /// <summary>
        /// 查询申请的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<WorkflowInfo> SelectApplyFlowPage(int pageIndex, int pageSize, ApplyFlowFilterInfo filter, string connectionId = null);

        /// <summary>
        /// 根据ID查询工作流信息且包含处理列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流信息且包含处理列表</returns>
        WorkflowInfo SelectContainHandles(int id, string connectionId = null);

        /// <summary>
        /// 根据ID更新流程状态和当前关卡、处理人信息
        /// </summary>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateFlowStatusAndCensorshipAndHandlerById(WorkflowInfo workflow, string connectionId = null);
    }
}
