using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Diversion;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流引擎基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FlowInT">流程输入类型</typeparam>
    public abstract partial class WorkflowEngineBase<FlowInT> : IWorkflowEngine<FlowInT>, IGetObject<IPersistenceConnection>
    {
        #region 属性与字段

        /// <summary>
        /// 表单引擎工厂
        /// </summary>
        public IFormEngineFactory FormEngineFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流持久化
        /// </summary>
        public IWorkflowPersistence WorkflowPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流处理持久化
        /// </summary>
        public IWorkflowHandlePersistence WorkflowHandlePersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流服务
        /// </summary>
        public IWorkflowService WorkflowService
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流处理
        /// </summary>
        public IWorkflowHandleService WorkflowHandle
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流配置读取
        /// </summary>
        public IWorkflowConfigReader WorkflowConfigReader
        {
            get;
            set;
        }

        /// <summary>
        /// 查找流程关卡
        /// </summary>
        public IFindFlowCensorship FindFlowCensorship
        {
            get;
            set;
        }

        /// <summary>
        /// 表单服务
        /// </summary>
        public IFormService FormService
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流定义服务
        /// </summary>
        public IWorkflowDefineService WorkflowDefineService
        {
            get;
            set;
        }

        /// <summary>
        /// 流程服务
        /// </summary>
        public IFlowService FlowService
        {
            get;
            set;
        }

        #endregion

        #region IGetObject<IPersistenceConnection>

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>
        public IPersistenceConnection Get() => WorkflowPersistence;

        #endregion

        #region IWorkflowEngine<FlowInT> 接口

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> Execute(FlowInT flowIn, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                WorkflowInfo workflow;
                WorkflowDefineInfo workflowDefine = ValiFlowIn(reInfo, flowIn, out workflow, connId, currUser);
                if (reInfo.Failure())
                {
                    return false;
                }

                FlowCensorshipInInfo findFlowCensorshipIn = new FlowCensorshipInInfo()
                {
                    WorkflowDefine = workflowDefine,
                    Workflow = workflow
                };
                AppendSetFindFlowCensorshipIn(flowIn, findFlowCensorshipIn);

                ReturnInfo<FlowCensorshipOutInfo> reOut = FindFlowCensorship.NextHandler(findFlowCensorshipIn, connId, currUser);
                if (reOut.Failure())
                {
                    reInfo.FromBasic(reOut);
                    return false;
                }

                ReturnInfo<bool> reTrans = ExecTransaction(reInfo, workflowDefine, flowIn, reOut.Data, connId, currUser);
                reInfo.FromBasic(reTrans);

                if (reTrans.Failure())
                {
                    return false;
                }

                return true;
            });
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflowDefine">工作流定义</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipOut">查找流程关卡输出</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Transaction(ConnectionIdIndex = 4)]
        protected virtual ReturnInfo<bool> ExecTransaction(ReturnInfo<bool> returnInfo, WorkflowDefineInfo workflowDefine,
            FlowInT flowIn, FlowCensorshipOutInfo findFlowCensorshipOut, string connectionId = null, BasicUserInfo<int> currUser = null)
        {          
            IFormEngine formEngine = FormEngineFactory.Create(workflowDefine.Code);
            if (formEngine == null)
            {
                returnInfo.SetFailureMsg($"找不到编码[{workflowDefine.Code}]的表单引擎");

                return returnInfo;
            }

            ReturnInfo<bool> basicReturn = formEngine.BeforeExecFlow(findFlowCensorshipOut, flowIn, connectionId, currUser);
            if (basicReturn.Failure())
            {
                returnInfo.FromBasic(basicReturn);

                return returnInfo;
            }

            ExecCore(returnInfo, flowIn, findFlowCensorshipOut, connectionId, currUser);

            basicReturn = formEngine.AfterExecFlow(findFlowCensorshipOut, flowIn, returnInfo.Success(), connectionId, currUser);
            if (basicReturn.Failure())
            {
                returnInfo.FromBasic(basicReturn);
            }

            return returnInfo;
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 验证流程输入
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>工作流定义</returns>
        protected abstract WorkflowDefineInfo ValiFlowIn(ReturnInfo<bool> returnInfo, FlowInT flowIn, out WorkflowInfo workflow, string connectionId = null, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        /// <param name="currUser">当前用户</param>
        protected abstract void AppendSetFindFlowCensorshipIn(FlowInT flowIn, FlowCensorshipInInfo findFlowCensorshipIn, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 执行核心
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipOut">查找流程关卡输出</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected abstract void ExecCore(ReturnInfo<bool> returnInfo, FlowInT flowIn, FlowCensorshipOutInfo findFlowCensorshipOut, string connectionId = null, BasicUserInfo<int> currUser = null);

        #endregion

        #region 执行返回连接ID的公共方法

        /// <summary>
        /// 执行返回函数且带有连接ID
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFuncAndConnectionId<OutT>(Func<ReturnInfo<OutT>, string, OutT> func, ReturnInfo<OutT> returnInfo = null, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            return ExecReturnFunc<OutT>((reInfo) =>
            {
                OutT result = default(OutT);
                ExecProcConnectionId((connId) =>
                {
                    result = func(reInfo, connId);
                }, connectionId, accessMode);

                return result;
            }, returnInfo);
        }

        /// <summary>
        /// 执行返回函数
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFunc<OutT>(Func<ReturnInfo<OutT>, OutT> func, ReturnInfo<OutT> returnInfo = null)
        {
            if (returnInfo == null)
            {
                returnInfo = new ReturnInfo<OutT>();
            }

            returnInfo.Data = func(returnInfo);

            return returnInfo;
        }

        /// <summary>
        /// 执行连接ID过程
        /// 如果传过来的连接ID为空，则会创建新的连接ID，结束后会自动注释连接ID，否则不会
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        protected void ExecProcConnectionId(Action<string> action, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                connectionId = WorkflowPersistence.NewConnectionId(accessMode);

                try
                {
                    action(connectionId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    WorkflowPersistence.Release(connectionId);
                }
            }
            else
            {
                action(connectionId);
                return;
            }
        }

        #endregion
    }
}
