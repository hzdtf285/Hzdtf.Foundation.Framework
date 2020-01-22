using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流移除基类
    /// @ 黄振东
    /// </summary>
    public abstract class WorkflowRemoveBase : IWorkflowEngine<int>, IGetObject<IPersistenceConnection>
    {
        #region 属性与字段

        /// <summary>
        /// 表单移除工厂
        /// </summary>
        public IFormRemoveFactory FormRemoveFactory
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
        /// 工作流定义持久化
        /// </summary>
        public IWorkflowDefinePersistence WorkflowDefinePersistence
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

        #region IWorkflowEngine<int> 接口

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Execute(int flowIn, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                var workflow = WorkflowPersistence.Select(flowIn, connId);
                if (workflow == null)
                {
                    reInfo.SetFailureMsg($"找不到ID[{flowIn}]的工作流");

                    return false;
                }

                workflow.WorkflowDefine = WorkflowDefinePersistence.Select(workflow.WorkflowDefineId, connId);
                if (workflow.WorkflowDefine == null)
                {
                    reInfo.SetFailureMsg($"找不到ID[{flowIn}]的工作流定义");

                    return false;
                }

                workflow.Handles = WorkflowHandlePersistence.SelectByWorkflowId(flowIn, connId);
                
                Vali(reInfo, workflow);
                if (reInfo.Failure())
                {
                    return false;
                }

                ReturnInfo<bool> reTrans = ExecTransaction(reInfo, workflow, connId);
                reInfo.FromBasic(reTrans);

                if (reTrans.Failure())
                {
                    return false;
                }

                var descAttr = typeof(RemoveType).GetAttributeForEnum<DescriptionAttribute>(GetRemoveType().ToString());
                if (descAttr != null)
                {
                    reInfo.SetSuccessMsg($"此工作流已{descAttr.Description}");
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
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Transaction(ConnectionIdIndex = 2)]
        protected virtual ReturnInfo<bool> ExecTransaction(ReturnInfo<bool> returnInfo, WorkflowInfo workflow, string connectionId = null)
        {
            IFormRemove formRemove = FormRemoveFactory.Create(workflow.WorkflowDefine.Code);
            if (formRemove == null)
            {
                returnInfo.SetFailureMsg($"找不到编码[{workflow.WorkflowDefine.Code}]的表单移除");

                return returnInfo;
            }

            ReturnInfo<bool> basicReturn = formRemove.BeforeExecFlow(workflow, GetRemoveType(), connectionId);
            if (basicReturn.Failure())
            {
                returnInfo.FromBasic(basicReturn);

                return returnInfo;
            }

            ExecCore(returnInfo, workflow, connectionId);

            basicReturn = formRemove.AfterExecFlow(workflow, GetRemoveType(), returnInfo.Success(), connectionId);
            if (basicReturn.Failure())
            {
                returnInfo.FromBasic(basicReturn);
            }

            return returnInfo;
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflow">工作流</param>
        protected virtual void Vali(ReturnInfo<bool> returnInfo, WorkflowInfo workflow) { }

        /// <summary>
        /// 执行核心
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflow">工作流</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void ExecCore(ReturnInfo<bool> returnInfo, WorkflowInfo workflow, string connectionId = null)
        {
            WorkflowHandlePersistence.DeleteByWorkflowId(workflow.Id, connectionId);
            WorkflowPersistence.DeleteById(workflow.Id, connectionId);
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取移除类型
        /// </summary>
        /// <returns>移除类型</returns>
        protected abstract RemoveType GetRemoveType();

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
