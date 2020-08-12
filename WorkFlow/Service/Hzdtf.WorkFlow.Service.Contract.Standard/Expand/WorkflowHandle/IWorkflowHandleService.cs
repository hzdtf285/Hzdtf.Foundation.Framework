using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard
{
    /// <summary>
    /// 工作流处理服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowHandleService
    {
        /// <summary>
        /// 根据ID修改为已读
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyToReadedById(int id, string connectionId = null, BasicUserInfo currUser = null);

        /// <summary>
        /// 根据处理人ID是否存在审核且未处理的个数
        /// </summary>
        /// <param name="handlerId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ExistsAuditAndUnhandleByHandleId(int handlerId, string connectionId = null, BasicUserInfo currUser = null);

        /// <summary>
        /// 根据处理人ID集合是否存在审核且未处理的个数
        /// </summary>
        /// <param name="handlerIds">处理人ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool[]> ExistsAuditAndUnhandleByHandleIds(int[] handlerIds, string connectionId = null, BasicUserInfo currUser = null);

        /// <summary>
        /// 根据工作流人ID、流程关卡ID和处理人ID查找工作流处理信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="handleId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowHandleInfo> FindByWorkflowIdAndFlowCensorshipIdAndHandlerId(int workflowId, int flowCensorshipId, int handleId, string connectionId = null, BasicUserInfo currUser = null);
    }
}
