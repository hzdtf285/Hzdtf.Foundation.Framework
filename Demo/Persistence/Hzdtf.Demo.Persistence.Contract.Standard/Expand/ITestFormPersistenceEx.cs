using Hzdtf.Demo.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Demo.Persistence.Contract.Standard
{
    /// <summary>
    /// 测试表单持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface ITestFormPersistence
    {
        /// <summary>
        /// 根据流程ID更新流程状态
        /// </summary>
        /// <param name="testForm">测试表单</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateFlowStatusByWorkflowId(TestFormInfo testForm, string connectionId = null);

        /// <summary>
        /// 根据工作流ID查询测试表单信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>测试表单信息</returns>
        TestFormInfo SelectByWorkflowId(int workflowId, string connectionId = null);

        /// <summary>
        /// 根据工作流ID删除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByWorkflowId(int workflowId, string connectionId = null);
    }
}
