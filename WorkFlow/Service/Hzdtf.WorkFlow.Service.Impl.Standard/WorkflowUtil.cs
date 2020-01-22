using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard
{
    /// <summary>
    /// 工作流辅助类
    /// @ 黄振东
    /// </summary>
    public static class WorkflowUtil
    {
        /// <summary>
        /// 判断当前用户能否审核
        /// </summary>
        /// <param name="workflowHandle">工作流处理</param>
        /// <returns>当前用户能否审核</returns>
        public static BasicReturnInfo CanCurrUserAudit(WorkflowHandleInfo workflowHandle)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();

            if (UserTool.CurrUser == null)
            {
                returnInfo.SetFailureMsg("您还未登录，请先登录系统");

                return returnInfo;
            }

            if (workflowHandle == null)
            {
                returnInfo.SetFailureMsg("找不到处理信息");

                return returnInfo;
            }

            if (workflowHandle.HandlerId != UserTool.CurrUser.Id)
            {
                returnInfo.SetFailureMsg("Sorry,您不是此流程的处理者,无权限审核");

                return returnInfo;
            }

            if (workflowHandle.HandleStatus == HandleStatusEnum.EFFICACYED)
            {
                returnInfo.SetFailureMsg("Sorry,您的处理信息已无效");

                return returnInfo;
            }
            if (workflowHandle.HandleStatus == HandleStatusEnum.SENDED || workflowHandle.HandleStatus == HandleStatusEnum.RETURNED)
            {
                returnInfo.SetFailureMsg("Sorry,您的处理信息已处理，无需重复处理");

                return returnInfo;
            }

            return returnInfo;
        }
    }
}
