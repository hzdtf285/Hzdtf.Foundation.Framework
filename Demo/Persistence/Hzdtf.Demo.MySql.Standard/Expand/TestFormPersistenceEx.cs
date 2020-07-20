using Hzdtf.Demo.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.Demo.MySql.Standard
{
    /// <summary>
    /// 测试表单持久化
    /// @ 黄振东
    /// </summary>
    public partial class TestFormPersistence
    {
        /// <summary>
        /// 根据流程ID更新流程状态
        /// </summary>
        /// <param name="testForm">测试表单</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateFlowStatusByWorkflowId(TestFormInfo testForm, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET {GetFieldByProp("FlowStatus")}=@FlowStatus{GetModifyInfoSql(testForm)} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId";
                result = dbConn.Execute(sql, testForm, GetDbTransaction(connId));
            });

            return result;
        }

        /// <summary>
        /// 根据工作流ID查询测试表单信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>测试表单信息</returns>
        public TestFormInfo SelectByWorkflowId(int workflowId, string connectionId = null)
        {
            TestFormInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("WorkflowId")}=@WorkflowId";
                result = dbConn.QueryFirstOrDefault<TestFormInfo>(sql, new { WorkflowId = workflowId }, GetDbTransaction(connId));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据工作流ID删除
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

        /// <summary>
        /// 获取查询分页连接SQL
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <returns>连接SQL语句</returns>
        protected override string GetSelectPageJoinSql(DynamicParameters parameters, FilterInfo filter = null)
        {
            return $"LEFT JOIN `workflow` w ON w.apply_no=`{Table}`.apply_no";
        }

        /// <summary>
        /// 追加查询分页字段SQL
        /// </summary>
        protected override string AppendSelectPageFieldsSql()
        {
            return ",w.`title` Title";
        }
    }
}
