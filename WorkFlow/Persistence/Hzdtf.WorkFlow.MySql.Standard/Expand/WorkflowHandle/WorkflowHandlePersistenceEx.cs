using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.WorkFlow.MySql.Standard
{
    /// <summary>
    /// 工作流处理持久化
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowHandlePersistence
    {
        /// <summary>
        /// 根据ID更新是否已读
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateIsReadedById(WorkflowHandleInfo workflowHandle, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("IsReaded")}`=@IsReaded{GetModifyInfoSql(workflowHandle)} WHERE {GetFieldByProp("Id") }=@Id";
                result = dbConn.Execute(sql, workflowHandle, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据ID更新处理状态
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateHandleStatusById(WorkflowHandleInfo workflowHandle, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("HandleStatus")}`=@HandleStatus,`{GetFieldByProp("HandleTime")}`=@HandleTime,`{GetFieldByProp("Idea")}`=@Idea" +
                $"{GetModifyInfoSql(workflowHandle)} WHERE {GetFieldByProp("Id") }=@Id";
                result = dbConn.Execute(sql, workflowHandle, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据工作流ID和处理状态统计个数但排除流程关卡ID
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="handleStatus">处理状态</param>
        /// <param name="notFlowCensorshipId">排除流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        public int CountNotFlowCensorshipIdByWorkflowIdAndHandleStatus(int workflowId, HandleStatusEnum handleStatus, int notFlowCensorshipId, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId AND {GetFieldByProp("HandleStatus")}=@handleStatus AND {GetFieldByProp("FlowCensorshipId")}!=@NotFlowCensorshipId";
                result = dbConn.ExecuteScalar<int>(sql, new { WorkflowId = workflowId, HandleStatus = (byte)handleStatus, NotFlowCensorshipId = notFlowCensorshipId }, GetDbTransaction(connId));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据工作流ID和流程关卡ID更新处理状态为已失效但排除ID
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateEfficacyedNotIdByWorkflowIdAndFlowCensorshipId(WorkflowHandleInfo workflowHandle, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("HandleStatus")}`={(byte)HandleStatusEnum.EFFICACYED},`{GetFieldByProp("HandleTime")}`=@HandleTime" +
                $"{GetModifyInfoSql(workflowHandle)} WHERE {GetFieldByProp("Id") }!=@Id AND {GetFieldByProp("WorkflowId") }=@WorkflowId" +
                $" AND {GetFieldByProp("HandleStatus") }={(byte)HandleStatusEnum.UN_HANDLE} AND {GetFieldByProp("HandleType")}={(byte)HandleTypeEnum.AUDIT}" +
                $" AND {GetFieldByProp("FlowCensorshipId")}=@FlowCensorshipId AND {GetFieldByProp("ConcreteConcreteId")}=@ConcreteConcreteId";
                result = dbConn.Execute(sql, workflowHandle, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据工作流ID更新处理状态为已失效但排除ID
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateEfficacyedNotIdByWorkflowId(WorkflowHandleInfo workflowHandle, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("HandleStatus")}`={(byte)HandleStatusEnum.EFFICACYED},`{GetFieldByProp("HandleTime")}`=@HandleTime" +
                $"{GetModifyInfoSql(workflowHandle)} WHERE {GetFieldByProp("Id") }!=@Id AND {GetFieldByProp("WorkflowId") }=@WorkflowId AND {GetFieldByProp("HandleStatus") }={(byte)HandleStatusEnum.UN_HANDLE} AND {GetFieldByProp("HandleType")}={(byte)HandleTypeEnum.AUDIT}";
                result = dbConn.Execute(sql, workflowHandle, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据工作流ID和流程关卡ID集合查询已经送件的工作流处理列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipIds">流程关卡ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>已经送件的工作流处理列表</returns>
        public IList<WorkflowHandleInfo> SelectSendedByWorkflowIdAndFlowCensorshipIds(int workflowId, int[] flowCensorshipIds, string connectionId = null)
        {
            IList<WorkflowHandleInfo> result = null;

            DynamicParameters parameters;
            string inSql = GetWhereIdsSql(flowCensorshipIds, out parameters, idField: GetFieldByProp("FlowCensorshipId"));
            parameters.Add("WorkflowId", workflowId);
            parameters.Add("HandleStatus", (byte)HandleStatusEnum.SENDED);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId AND {GetFieldByProp("HandleStatus")}=@HandleStatus AND {inSql}";
                result = dbConn.Query<WorkflowHandleInfo>(sql, parameters, GetDbTransaction(connId)).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据处理人ID统计审核且未处理的个数
        /// </summary>
        /// <param name="handlerId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>审核中且未处理的个数</returns>
        public int CountAuditAndUnhandleByHandleId(int handlerId, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE {GetFieldByProp("HandlerId")}=@HandlerId AND {GetFieldByProp("HandleStatus")}=@handleStatus AND {GetFieldByProp("HandleType")}=@HandleType";
                result = dbConn.ExecuteScalar<int>(sql, new { HandlerId = handlerId, HandleStatus = (byte)HandleStatusEnum.UN_HANDLE, HandleType = (byte)HandleTypeEnum.AUDIT }, GetDbTransaction(connId));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据工作流ID、流程关卡ID和处理人ID查询工作流处理信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="handleId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流处理信息</returns>
        public WorkflowHandleInfo SelectByWorkflowIdAndFlowCensorshipIdAndHandlerId(int workflowId, int flowCensorshipId, int handleId, string connectionId = null)
        {
            WorkflowHandleInfo result = null;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("WorkflowId", workflowId);
            parameters.Add("FlowCensorshipId", flowCensorshipId);
            parameters.Add("HandlerId", handleId);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId AND {GetFieldByProp("FlowCensorshipId")}=@FlowCensorshipId AND {GetFieldByProp("HandlerId")}=@HandlerId";
                result = dbConn.QueryFirstOrDefault<WorkflowHandleInfo>(sql, parameters, GetDbTransaction(connId));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据工作流ID查询工作流处理信息列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流处理信息列表</returns>
        public IList<WorkflowHandleInfo> SelectByWorkflowId(int workflowId, string connectionId = null)
        {
            IList<WorkflowHandleInfo> result = null;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("WorkflowId", workflowId);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId";
                result = dbConn.Query<WorkflowHandleInfo>(sql, parameters, GetDbTransaction(connId)).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据工作流ID删除工作流处理信息列表
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int DeleteByWorkflowId(int workflowId, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{DeleteSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId";
                result = dbConn.Execute(sql, new { WorkflowId = workflowId }, GetDbTransaction(connId));
            });

            return result;
        }
    }
}
