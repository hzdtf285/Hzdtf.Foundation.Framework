using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Service.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.WorkFlow.Standard
{
    /// <summary>
    /// 用户工作流辅助类
    /// @ 黄振东
    /// </summary>
    public static class UserWorkflowUtil
    {
        /// <summary>
        /// 初始化用户处理验证
        /// 注册用户删除前判断是否有处理的事件
        /// </summary>
        public static void InitValiUserHandleVali()
        {
            IUserService userService = AutofacTool.Resolve<IUserService>();
            userService.RemoveByIding += UserService_RemoveByIding;
            userService.RemoveByIdsing += UserService_RemoveByIdsing;
        }

        /// <summary>
        /// 根据用户ID移除前事件
        /// </summary>
        /// <param name="arg1">返回信息</param>
        /// <param name="arg2">用户ID</param>
        /// <param name="arg3">连接ID</param>
        private static void UserService_RemoveByIdsing(ReturnInfo<bool> arg1, int[] arg2, string arg3)
        {
            IWorkflowHandleService workflowHandleService = AutofacTool.Resolve<IWorkflowHandleService>();
            ReturnInfo<bool[]> handleReturnInfo = workflowHandleService.ExistsAuditAndUnhandleByHandleIds(arg2, arg3);
            if (handleReturnInfo.Failure())
            {
                arg1.FromBasic(handleReturnInfo);
                return;
            }

            if (handleReturnInfo.Data.IsNullOrLength0())
            {
                return;
            }

            for (var i = 0; i < handleReturnInfo.Data.Length; i++)
            {
                if (handleReturnInfo.Data[i])
                {
                    arg1.SetFailureMsg($"第{i + 1}行：用户尚有未处理的审核流程，故不能移除");
                    return;
                }
            }
        }

        /// <summary>
        /// 根据用户ID移除前事件
        /// </summary>
        /// <param name="arg1">返回信息</param>
        /// <param name="arg2">用户ID</param>
        /// <param name="arg3">连接ID</param>
        private static void UserService_RemoveByIding(ReturnInfo<bool> arg1, int arg2, string arg3)
        {
            IWorkflowHandleService workflowHandleService = AutofacTool.Resolve<IWorkflowHandleService>();
            ReturnInfo<bool> handleReturnInfo = workflowHandleService.ExistsAuditAndUnhandleByHandleId(arg2, arg3);
            if (handleReturnInfo.Failure())
            {
                arg1.FromBasic(handleReturnInfo);
                return;
            }

            if (handleReturnInfo.Data)
            {
                arg1.SetFailureMsg("用户尚有未处理的审核流程，故不能移除");
            }
        }
    }
}
