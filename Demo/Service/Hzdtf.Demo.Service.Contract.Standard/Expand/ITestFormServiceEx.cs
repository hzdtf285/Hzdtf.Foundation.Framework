using Hzdtf.Demo.Model.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Demo.Service.Contract.Standard
{
    /// <summary>
    /// 测试表单服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface ITestFormService
    {
        /// <summary>
        /// 根据流程ID修改流程状态
        /// </summary>
        /// <param name="testForm">测试表单</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyFlowStatusByWorkflowId(TestFormInfo testForm, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
