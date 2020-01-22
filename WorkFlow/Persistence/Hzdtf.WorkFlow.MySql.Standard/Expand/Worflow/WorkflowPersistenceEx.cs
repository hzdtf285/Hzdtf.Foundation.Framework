using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand.Filter;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.WorkFlow.MySql.Standard
{
    /// <summary>
    /// 工作流持久化接口
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowPersistence        
    {
        /// <summary>
        /// 工作流处理持久化
        /// </summary>
        public IWorkflowHandlePersistence WorkflowHandlePersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 根据申请单号统计记录数
        /// </summary>
        /// <param name="applyNo">申请单号</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>记录数</returns>
        public int CountByApplyNo(string applyNo, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE {GetFieldByProp("ApplyNo")}=@ApplyNo";
                result = dbConn.ExecuteScalar<int>(sql, new { ApplyNo = applyNo });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询待办的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public PagingInfo<WorkflowInfo> SelectWaitHandlePage(int pageIndex, int pageSize, WaitHandleFilterInfo filter, string connectionId = null)
        {
            PagingInfo<WorkflowInfo> result = null;
            DynamicParameters parameters;
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);

            whereSql.Append(" AND wh.handler_id=@HandlerId AND flow_status!=@NotFlowStatus AND ((wh.handle_status=@HandleStatus && handle_type=@HandleType1) || (is_readed=@IsReaded && handle_type=@HandleType2))");
            parameters.Add("HandlerId", filter.HandlerId);
            parameters.Add("NotFlowStatus", (byte)FlowStatusEnum.DRAFT);
            parameters.Add("HandleStatus", (byte)HandleStatusEnum.UN_HANDLE);
            parameters.Add("HandleType1", (byte)HandleTypeEnum.AUDIT);
            parameters.Add("HandleType2", (byte)HandleTypeEnum.NOTIFY);
            parameters.Add("IsReaded", false);

            if (filter.HandleType != null)
            {
                whereSql.Append(" AND wh.handle_type=@HandleType");
                parameters.Add("HandleType", filter.HandleType);
            }
            if (filter.IsReaded != null)
            {
                whereSql.Append(" AND wh.is_readed=@IsReaded");
                parameters.Add("IsReaded", filter.IsReaded);
            }

            string sortSql = GetSelectPageSortSql(filter, GetSelectSortNamePfx(filter));
            if (string.IsNullOrWhiteSpace(sortSql))
            {
                sortSql = $" ORDER BY wh.is_readed,wh.create_time DESC";
            }

            string formatSql = $"SELECT {{0}} FROM {Table}"
                                + $" INNER JOIN workflow_handle wh ON {Table}.`id`=wh.`workflow_id`" 
                                + whereSql.ToString();

            string countSql = string.Format(formatSql, "COUNT(1)");
            string pageSql = string.Format(formatSql, $"{JoinSelectPropMapFields(pfx: Table + ".")},wh.`id`,wh.`is_readed` IsReaded,wh.`handle_type` HandleType,wh.`handle_status` HandleStatus")
                 + " " + sortSql + " " + GetPartPageSql(pageIndex, pageSize);
            
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = PagingUtil.ExecPage<WorkflowInfo>(pageIndex, pageSize, () =>
                {
                    return dbConn.ExecuteScalar<int>(countSql, parameters, GetDbTransaction(connId));
                }, () =>
                {
                    return dbConn.Query<WorkflowInfo, WorkflowHandleInfo, WorkflowInfo>(pageSql, (wf, wh) =>
                    {
                        if (wf.Handles == null)
                        {
                            wf.Handles = new List<WorkflowHandleInfo>()
                            {
                                wh
                            };
                        }

                        return wf;

                    }, parameters, GetDbTransaction(connId), splitOn: "id").AsList();
                });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询已审核的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public PagingInfo<WorkflowInfo> SelectAuditedHandlePage(int pageIndex, int pageSize, AuditFlowFilterInfo filter, string connectionId = null)
        {
            PagingInfo<WorkflowInfo> result = null;
            DynamicParameters parameters;
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);

            whereSql.Append(" AND wh.handler_id=@HandlerId AND (wh.handle_status=@HandleStatus1 || wh.handle_status=@HandleStatus2) && handle_type=@HandleType");
            parameters.Add("HandlerId", filter.HandlerId);
            parameters.Add("HandleStatus1", (byte)HandleStatusEnum.SENDED);
            parameters.Add("HandleStatus2", (byte)HandleStatusEnum.RETURNED);
            parameters.Add("HandleType", (byte)HandleTypeEnum.AUDIT);

            if (filter.FlowStatus != null)
            {
                whereSql.Append(" AND flow_status=@FlowStatus");
                parameters.Add("FlowStatus", filter.FlowStatus);
            }

            string sortSql = GetSelectPageSortSql(filter, GetSelectSortNamePfx(filter));
            if (string.IsNullOrWhiteSpace(sortSql))
            {
                sortSql = $" ORDER BY wh.handle_time DESC";
            }

            string formatSql = $"SELECT {{0}} FROM {Table}"
                                + $" INNER JOIN workflow_handle wh ON {Table}.`id`=wh.`workflow_id`"
                                + whereSql.ToString();

            string countSql = string.Format(formatSql, "COUNT(1)");
            string pageSql = string.Format(formatSql, $"{JoinSelectPropMapFields(pfx: Table + ".")},wh.`id`,wh.`is_readed` IsReaded")
                 + " " + sortSql + " " + GetPartPageSql(pageIndex, pageSize);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = PagingUtil.ExecPage<WorkflowInfo>(pageIndex, pageSize, () =>
                {
                    return dbConn.ExecuteScalar<int>(countSql, parameters, GetDbTransaction(connId));
                }, () =>
                {
                    return dbConn.Query<WorkflowInfo, WorkflowHandleInfo, WorkflowInfo>(pageSql, (wf, wh) =>
                    {
                        if (wf.Handles == null)
                        {
                            wf.Handles = new List<WorkflowHandleInfo>()
                            {
                                wh
                            };
                        }

                        return wf;

                    }, parameters, GetDbTransaction(connId), splitOn: "id").AsList();
                });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询申请的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public PagingInfo<WorkflowInfo> SelectApplyFlowPage(int pageIndex, int pageSize, ApplyFlowFilterInfo filter, string connectionId = null)
        {
            PagingInfo<WorkflowInfo> result = null;
            DynamicParameters parameters;
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);

            whereSql.Append(" AND creater_id=@HandlerId");
            parameters.Add("HandlerId", filter.HandlerId);

            if (filter.FlowStatus != null)
            {
                whereSql.Append(" AND flow_status=@FlowStatus");
                parameters.Add("FlowStatus", filter.FlowStatus);
            }

            string sortSql = GetSelectPageSortSql(filter, GetSelectSortNamePfx(filter));
            if (string.IsNullOrWhiteSpace(sortSql))
            {
                sortSql = $" ORDER BY create_time DESC";
            }

            string formatSql = $"SELECT {{0}} FROM {Table}"
                                + whereSql.ToString();

            string countSql = string.Format(formatSql, "COUNT(1)");
            string pageSql = string.Format(formatSql, $"{JoinSelectPropMapFields()}")
                 + " " + sortSql + " " + GetPartPageSql(pageIndex, pageSize);

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = PagingUtil.ExecPage<WorkflowInfo>(pageIndex, pageSize, () =>
                {
                    return dbConn.ExecuteScalar<int>(countSql, parameters, GetDbTransaction(connId));
                }, () =>
                {
                    return dbConn.Query<WorkflowInfo>(pageSql, parameters, GetDbTransaction(connId)).AsList();
                });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID查询工作流信息且包含处理列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流信息且包含处理列表</returns>
        public WorkflowInfo SelectContainHandles(int id, string connectionId = null)
        {
            WorkflowInfo result = null;
            string wFields = JoinSelectPropMapFields(pfx: "w.");

            string[] props = WorkflowHandlePersistence.AllFieldMapProps();
            IList<string> pList = new List<string>(props.Length - 1);
            pList.Add("id Id");
            foreach (var p in props)
            {
                if ("id Id".Equals(p))
                {
                    continue;
                }

                pList.Add(p);
            }
            string hFields = JoinSelectPropMapFields(pList.ToArray(), "h.");

            string sql = $"SELECT {wFields},{hFields} FROM workflow w"
                        + " INNER JOIN workflow_handle h ON w.id = h.workflow_id"
                        + " WHERE w.id=@Id";
            
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                dbConn.Query<WorkflowInfo, WorkflowHandleInfo, WorkflowInfo>(sql, (x, h) =>
                {
                    if (result == null)
                    {
                        result = x;
                        result.Handles = new List<WorkflowHandleInfo>();
                    }
                    result.Handles.Add(h);

                    return result;
                }, new { Id = id }, GetDbTransaction(connId), splitOn: "id");
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID更新流程状态和当前关卡、处理人信息
        /// </summary>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateFlowStatusAndCensorshipAndHandlerById(WorkflowInfo workflow, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("FlowStatus")}`=@FlowStatus," +
                        $"`{GetFieldByProp("CurrConcreteCensorshipIds")}`=@CurrConcreteCensorshipIds,`{GetFieldByProp("CurrConcreteCensorships")}`=@CurrConcreteCensorships,`{GetFieldByProp("CurrFlowCensorshipIds")}`=@CurrFlowCensorshipIds," +
                        $"`{GetFieldByProp("CurrHandlerIds")}`=@CurrHandlerIds,`{GetFieldByProp("CurrHandlers")}`=@CurrHandlers" + 
                        $" {GetModifyInfoSql(workflow)} WHERE {GetFieldByProp("Id") }=@Id";
                result = dbConn.Execute(sql, workflow, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected override string[] GetPageKeywordFields() => new string[] { GetFieldByProp("ApplyNo"), GetFieldByProp("Title") };
    }
}
