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
    /// 退件流程路线持久化
    /// @ 黄振东
    /// </summary>
    public partial class ReturnFlowRoutePersistence
    {
        /// <summary>
        /// 根据流程关卡ID查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>退件流程路线列表</returns>
        public IList<ReturnFlowRouteInfo> SelectByFlowCensorshipId(int flowCensorshipId, string connectionId = null)
        {
            IList<ReturnFlowRouteInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("FlowCensorshipId")}=@FlowCensorshipId";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByFlowCensorshipId");
                result = dbConn.Query<ReturnFlowRouteInfo>(sql, new { FlowCensorshipId = flowCensorshipId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据流程关卡ID数组查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>退件流程路线列表</returns>
        public IList<ReturnFlowRouteInfo> SelectByFlowCensorshipIds(int[] flowCensorshipIds, string connectionId = null)
        {
            IList<ReturnFlowRouteInfo> result = null;
            DynamicParameters parameters;
            string idSql = GetWhereIdsSql(flowCensorshipIds, out parameters, null, GetFieldByProp("FlowCensorshipId"));
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {idSql}";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByFlowCensorshipIds");
                result = dbConn.Query<ReturnFlowRouteInfo>(sql, parameters).AsList();
            }, AccessMode.SLAVE);

            return result;
        }
    }
}
