using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.WorkFlow.Model.Standard.Expand;

namespace Hzdtf.WorkFlow.Service.Impl.Standard
{
    /// <summary>
    /// 工作流服务
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowService
    {
        /// <summary>
        /// 工作流配置读取
        /// </summary>
        public IWorkflowConfigReader WorkflowConfigReader
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据读取工厂
        /// </summary>
        public IFormDataReaderFactory FormDataReaderFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 根据申请单号判断是否存在
        /// </summary>
        /// <param name="applyNo">申请单号</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>申请单号判断是否存在</returns>
        [Auth]
        public virtual ReturnInfo<bool> ExistsByApplyNo([DisplayName2("申请单号"), Required] string applyNo, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                return Persistence.CountByApplyNo(applyNo, connId) > 0;
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 查询当前用户的待办的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<PagingInfo<WorkflowInfo>> QueryCurrUserWaitHandlePage(int pageIndex, int pageSize, WaitHandleFilterInfo filter, string connectionId = null)
        {
            if (filter == null)
            {
                filter = new WaitHandleFilterInfo();
            }
            filter.HandlerId = UserTool.CurrUser.Id;
            filter.EndCreateTime = filter.EndCreateTime.AddThisDayLastTime();

            return ExecReturnFuncAndConnectionId<PagingInfo<WorkflowInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectWaitHandlePage(pageIndex, pageSize, filter, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 查询当前用户的已审核的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<PagingInfo<WorkflowInfo>> QueryCurrUserAuditedFlowPage(int pageIndex, int pageSize, AuditFlowFilterInfo filter, string connectionId = null)
        {
            if (filter == null)
            {
                filter = new AuditFlowFilterInfo();
            }
            filter.HandlerId = UserTool.CurrUser.Id;
            filter.EndCreateTime = filter.EndCreateTime.AddThisDayLastTime();

            return ExecReturnFuncAndConnectionId<PagingInfo<WorkflowInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectAuditedHandlePage(pageIndex, pageSize, filter, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 查询当前用户的申请的工作流列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<PagingInfo<WorkflowInfo>> QueryCurrUserApplyFlowPage(int pageIndex, int pageSize, ApplyFlowFilterInfo filter, string connectionId = null)
        {
            if (filter == null)
            {
                filter = new ApplyFlowFilterInfo();
            }
            filter.HandlerId = UserTool.CurrUser.Id;
            filter.EndCreateTime = filter.EndCreateTime.AddThisDayLastTime();

            return ExecReturnFuncAndConnectionId<PagingInfo<WorkflowInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectApplyFlowPage(pageIndex, pageSize, filter, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 根据ID查找工作流信息且包含处理列表和所有配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<WorkflowInfo> FindContainHandlesAndAllConfigs([DisplayName2("ID"), Id] int id, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<WorkflowInfo>((reInfo, connId) =>
            {
                WorkflowInfo workflow = Persistence.SelectContainHandles(id, connId);
                if (workflow == null)
                {
                    reInfo.SetFailureMsg("找不到此工作流信息");

                    return null;
                }
                if (workflow.Handles.IsNullOrCount0())
                {
                    reInfo.SetFailureMsg("找不到此工作流处理信息");

                    return null;
                }

                ReturnInfo<WorkflowDefineInfo> reDefine = WorkflowConfigReader.ReaderAllConfig(workflow.WorkflowDefineId, connId);
                if (reDefine.Failure())
                {
                    reInfo.FromBasic(reDefine);

                    return null;
                }
                if (reDefine.Data == null)
                {
                    reInfo.SetFailureMsg("找不到此工作流定义信息");

                    return null;
                }

                ReturnInfo<ConcreteFormInfo> reFormData = FormDataReaderFactory.Create(reDefine.Data.Code).ReaderByWorkflowId(id, connId);
                if (reFormData.Failure())
                {
                    reInfo.FromBasic(reDefine);

                    return null;
                }
                workflow.FormData = reFormData.Data;

                workflow.WorkflowDefine = reDefine.Data;

                return workflow;
            }, null, connectionId, AccessMode.SLAVE);
        }

        /// <summary>
        /// 根据ID查找审核明细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="handleId">处理ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<WorkflowInfo> FindAuditDetail([DisplayName2("ID"), Id] int id, [DisplayName2("处理ID"), Id] int handleId, string connectionId = null)
        {
            ReturnInfo<WorkflowInfo> returnInfo = FindContainHandlesAndAllConfigs(id, connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            WorkflowHandleInfo currHandle = null;
            foreach (var h in returnInfo.Data.Handles)
            {
                if (h.Id == handleId)
                {
                    currHandle = h;
                    break;
                }
            }

            BasicReturnInfo basicReturn = WorkflowUtil.CanCurrUserAudit(currHandle);
            if (basicReturn.Failure())
            {
                returnInfo.FromBasic(basicReturn);
            }

            return returnInfo;
        }

        /// <summary>
        /// 根据ID查找待审核明细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="handleId">处理ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<WorkflowInfo> FindWaitDetail([DisplayName2("ID"), Id] int id, [DisplayName2("处理ID"), Id] int handleId, string connectionId = null)
        {
            ReturnInfo<WorkflowInfo> returnInfo = FindContainHandlesAndAllConfigs(id, connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            WorkflowHandleInfo currHandle = null;
            foreach (var h in returnInfo.Data.Handles)
            {
                if (h.Id == handleId)
                {
                    currHandle = h;
                    break;
                }
            }
            if (currHandle.HandleStatus == HandleStatusEnum.EFFICACYED)
            {
                returnInfo.SetFailureMsg("您还处理的流程已失效");
                return returnInfo;
            }

            return returnInfo;
        }

        /// <summary>
        /// 根据ID查找已审核明细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="handleId">处理ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<WorkflowInfo> FindAuditedDetail([DisplayName2("ID"), Id] int id, [DisplayName2("处理ID"), Id] int handleId, string connectionId = null)
        {
            ReturnInfo<WorkflowInfo> returnInfo = FindContainHandlesAndAllConfigs(id, connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            WorkflowHandleInfo currHandle = null;
            foreach (var h in returnInfo.Data.Handles)
            {
                if (h.Id == handleId)
                {
                    currHandle = h;
                    break;
                }
            }

            if (currHandle.HandleStatus == HandleStatusEnum.UN_HANDLE)
            {
                returnInfo.SetFailureMsg("您还未处理该流程");
                return returnInfo;
            }
            if (currHandle.HandleStatus == HandleStatusEnum.EFFICACYED)
            {
                returnInfo.SetFailureMsg("您还处理的流程已失效");
                return returnInfo;
            }

            return returnInfo;
        }

        /// <summary>
        /// 根据ID查找当前用户申请明细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<WorkflowInfo> FindCurrUserApplyDetail([DisplayName2("ID"), Id] int id, string connectionId = null)
        {
            ReturnInfo<WorkflowInfo> returnInfo = FindContainHandlesAndAllConfigs(id, connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            if (returnInfo.Data.CreaterId != UserTool.CurrUser.Id)
            {
                returnInfo.SetFailureMsg("此工作流不是您申请的，无权限查看");

                return returnInfo;
            }

            return returnInfo;
        }
    }
}
