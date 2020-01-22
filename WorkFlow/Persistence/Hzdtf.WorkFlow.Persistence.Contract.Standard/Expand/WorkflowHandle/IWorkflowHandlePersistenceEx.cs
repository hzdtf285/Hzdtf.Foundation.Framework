using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 工作流处理持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowHandlePersistence
    {
        /// <summary>
        /// 根据ID更新是否已读
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateIsReadedById(WorkflowHandleInfo workflowHandle, string connectionId = null);

        /// <summary>
        /// 根据ID更新处理状态
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateHandleStatusById(WorkflowHandleInfo workflowHandle, string connectionId = null);

        /// <summary>
        /// 根据工作流ID和处理状态统计个数但排除流程关卡ID
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="handleStatus">处理状态</param>
        /// <param name="notFlowCensorshipId">排除流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        int CountNotFlowCensorshipIdByWorkflowIdAndHandleStatus(int workflowId, HandleStatusEnum handleStatus, int notFlowCensorshipId, string connectionId = null);

        /// <summary>
        /// 根据工作流ID和流程关卡ID更新处理状态为已失效但排除ID
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateEfficacyedNotIdByWorkflowIdAndFlowCensorshipId(WorkflowHandleInfo workflowHandle, string connectionId = null);

        /// <summary>
        /// 根据工作流ID更新处理状态为已失效但排除ID
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateEfficacyedNotIdByWorkflowId(WorkflowHandleInfo workflowHandle, string connectionId = null);

        /// <summary>
        /// 根据工作流ID和流程关卡ID集合查询已经送件的工作流处理列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipIds">流程关卡ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>已经送件的工作流处理列表</returns>
        IList<WorkflowHandleInfo> SelectSendedByWorkflowIdAndFlowCensorshipIds(int workflowId, int[] flowCensorshipIds, string connectionId = null);

        /// <summary>
        /// 根据处理人ID统计审核中且未处理的个数
        /// </summary>
        /// <param name="handlerId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>审核中且未处理的个数</returns>
        int CountAuditAndUnhandleByHandleId(int handlerId, string connectionId = null);

        /// <summary>
        /// 根据工作流ID、流程关卡ID和处理人ID查询工作流处理信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="handleId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流处理信息</returns>
        WorkflowHandleInfo SelectByWorkflowIdAndFlowCensorshipIdAndHandlerId(int workflowId, int flowCensorshipId, int handleId, string connectionId = null);

        /// <summary>
        /// 根据工作流ID查询工作流处理信息列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流处理信息列表</returns>
        IList<WorkflowHandleInfo> SelectByWorkflowId(int workflowId, string connectionId = null);

        /// <summary>
        /// 根据工作流ID删除工作流处理信息列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByWorkflowId(int workflowId, string connectionId = null);
    }
}
