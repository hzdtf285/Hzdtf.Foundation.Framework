using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Diversion;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine.Diversion
{
    /// <summary>
    /// 查找流程关卡
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class FindFlowCensorship : IFindFlowCensorship
    {
        #region 属性与字段

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
        /// 查找处理者上一级用户
        /// </summary>
        public IFindHandlerSupervisorUser FindHandlerSupervisorUser
        {
            get;
            set;
        }

        /// <summary>
        /// 查找处理者角色用户
        /// </summary>
        public IFindHandlerRoleUser FindHandlerRoleUser
        {
            get;
            set;
        }

        /// <summary>
        /// 查找处理者具体用户
        /// </summary>
        public IFindHandlerConcreteUser FindHandlerConcreteUser
        {
            get;
            set;
        }

        #endregion

        #region IFindFlowCensorship 接口

        /// <summary>
        /// 查找下一个处理信息
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<FlowCensorshipOutInfo> NextHandler(FlowCensorshipInInfo findFlowCensorshipIn, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<FlowCensorshipOutInfo> returnInfo = new ReturnInfo<FlowCensorshipOutInfo>();

            if (findFlowCensorshipIn == null)
            {
                returnInfo.SetFailureMsg("查找流程关卡输入不能为null");
                return returnInfo;
            }
            if (findFlowCensorshipIn.WorkflowDefine == null)
            {
                returnInfo.SetFailureMsg("工作流定义不能为null");
                return returnInfo;
            }

            returnInfo.Data = new FlowCensorshipOutInfo();
            returnInfo.Data.ActionType = findFlowCensorshipIn.ActionType;

            if (findFlowCensorshipIn.CurrWorkflowHandle == null || findFlowCensorshipIn.Workflow == null || findFlowCensorshipIn.Workflow.FlowStatus == FlowStatusEnum.DRAFT)
            {
                ApplyHandle(returnInfo, findFlowCensorshipIn, connectionId, currUser);
            }
            else
            {
                AuditHandle(returnInfo, findFlowCensorshipIn, connectionId, currUser);
            }

            return returnInfo;
        }

        #endregion

        #region 受保护的方法

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

        #region 私有方法

        /// <summary>
        /// 申请者处理
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        private void ApplyHandle(ReturnInfo<FlowCensorshipOutInfo> returnInfo, FlowCensorshipInInfo findFlowCensorshipIn, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            if (findFlowCensorshipIn.ActionType == ActionType.RETURN)
            {
                returnInfo.SetFailureMsg("申请阶段必须不能退件");
                return;
            }

            // 查找申请者流程关卡ID
            StandardCensorshipInfo applyStand = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);
            if (applyStand == null)
            {
                returnInfo.SetFailureMsg("找不到申请者的标准关卡");
                return;
            }

            var user = UserTool<int>.GetCurrUser(currUser);

            if (returnInfo.Data.Workflow == null)
            {
                if (findFlowCensorshipIn.Workflow == null)
                {
                    returnInfo.Data.Workflow = new WorkflowInfo()
                    {
                        ApplyNo = findFlowCensorshipIn.ApplyNo,
                        Title = findFlowCensorshipIn.Title,
                        WorkflowDefineId = findFlowCensorshipIn.WorkflowDefine.Id
                    };
                    returnInfo.Data.Workflow.SetCreateInfo(currUser);
                }
                else
                {
                    returnInfo.Data.Workflow = findFlowCensorshipIn.Workflow;
                    returnInfo.Data.Workflow.SetModifyInfo(currUser);
                }
            }
            else
            {
                returnInfo.Data.Workflow.SetModifyInfo(currUser);
            }
            returnInfo.Data.Workflow.FlowStatus = findFlowCensorshipIn.ActionType == ActionType.SEND ? FlowStatusEnum.AUDITING : FlowStatusEnum.DRAFT;
            string idea = findFlowCensorshipIn.Idea;
            returnInfo.Data.Workflow.Title = findFlowCensorshipIn.Title;

            // 申请者需要构造一条申请处理的记录
            FlowCensorshipInfo applyFlowCensors = FindFlowCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);

            // 送件才需要找下一个处理
            if (findFlowCensorshipIn.ActionType == ActionType.SEND)
            {
                FlowCensorshipInfo[] nextSendFlowCensorships = FindSendNextFlowCensorships(returnInfo, findFlowCensorshipIn, applyFlowCensors.Id);
                if (nextSendFlowCensorships.IsNullOrLength0())
                {
                    returnInfo.SetFailureMsg("找不到申请者的下一个处理流程关卡");
                    return;
                }

                ConcreteCensorshipInfo[] concreteCensorships = FindMappingConcreteCensorships(returnInfo, findFlowCensorshipIn, nextSendFlowCensorships, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return;
                }

                UpdateCurrWorkflowHandleInfo(concreteCensorships, returnInfo.Data.Workflow);

                returnInfo.Data.NextConcreteCensorshipHandles = concreteCensorships;

                if (string.IsNullOrWhiteSpace(idea))
                {
                    idea = "提交申请";
                }
            }
            else
            {
                returnInfo.Data.Workflow.CurrConcreteCensorshipIds = applyStand.Id.ToString();
                returnInfo.Data.Workflow.CurrConcreteCensorships = applyStand.Name;
                returnInfo.Data.Workflow.CurrFlowCensorshipIds = applyFlowCensors.Id.ToString();
                returnInfo.Data.Workflow.CurrHandlerIds = user.Id.ToString();
                returnInfo.Data.Workflow.CurrHandlers = user.Name;

                if (string.IsNullOrWhiteSpace(idea))
                {
                    idea = "保存草稿";
                }
            }

            StandardCensorshipInfo applyStandCensors = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);
            ConcreteCensorshipInfo applyConsors = new ConcreteCensorshipInfo()
            {
                Id = applyFlowCensors.Id,
                Code = applyStandCensors.Code,
                Name = applyStandCensors.Name,
                WorkflowHandles = new WorkflowHandleInfo[]
                {
                    new WorkflowHandleInfo()
                    {
                        ConcreteConcrete = applyStandCensors.Name,
                        ConcreteConcreteId = applyStandCensors.Id,
                        FlowCensorshipId = applyFlowCensors.Id,
                        Handler = user.Name,
                        HandlerId = user.Id,
                        HandleStatus = findFlowCensorshipIn.ActionType == ActionType.SEND ? HandleStatusEnum.SENDED : HandleStatusEnum.UN_HANDLE,
                        Idea = idea,
                        IsReaded = true,
                        HandleType = HandleTypeEnum.APPLY,
                        HandleTime = DateTime.Now
                    }
                },
                FlowCensorship = applyFlowCensors
            };
            if (findFlowCensorshipIn.ActionType == ActionType.SEND)
            {
                applyConsors.WorkflowHandles[0].HandleTime = DateTime.Now;
            }

            applyConsors.WorkflowHandles[0].SetCreateInfo(currUser);
            returnInfo.Data.CurrConcreteCensorship = applyConsors;
        }

        /// <summary>
        /// 审核处理
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        private void AuditHandle(ReturnInfo<FlowCensorshipOutInfo> returnInfo, FlowCensorshipInInfo findFlowCensorshipIn, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            if (findFlowCensorshipIn.CurrWorkflowHandle == null)
            {
                returnInfo.SetFailureMsg("当前工作流处理不能为null");
                return;
            }
            
            if (findFlowCensorshipIn.ActionType == ActionType.SAVE)
            {
                returnInfo.SetFailureMsg("审核阶段不能保存");
                return;
            }

            returnInfo.Data.Workflow = findFlowCensorshipIn.Workflow;

            FlowCensorshipInfo currFlowCens = FindFlowCensorshipByFlowCensorshipId(findFlowCensorshipIn, findFlowCensorshipIn.CurrWorkflowHandle.FlowCensorshipId);
            if (currFlowCens == null)
            {
                returnInfo.SetFailureMsg("找不到当前处理的流程关卡");
                return;
            }

            findFlowCensorshipIn.CurrWorkflowHandle.HandleTime = DateTime.Now;

            returnInfo.Data.CurrConcreteCensorship = new ConcreteCensorshipInfo()
            {
                Id = findFlowCensorshipIn.CurrWorkflowHandle.ConcreteConcreteId,
                Name = findFlowCensorshipIn.CurrWorkflowHandle.ConcreteConcrete,
                WorkflowHandles = new WorkflowHandleInfo[] { findFlowCensorshipIn.CurrWorkflowHandle },
                FlowCensorship = currFlowCens
            };

            string idea = findFlowCensorshipIn.Idea;
            if (findFlowCensorshipIn.ActionType == ActionType.SEND)
            {
                if (string.IsNullOrWhiteSpace(idea))
                {
                    idea = "送件";
                }

                findFlowCensorshipIn.CurrWorkflowHandle.Idea = idea;
                findFlowCensorshipIn.CurrWorkflowHandle.HandleStatus = HandleStatusEnum.SENDED;

                // 查找是否存在其他并行流程关卡未处理状态，如果有则只更新本次处理为已处理状态，不往下一关卡发送
                int existsOtherUnHandleCount = WorkflowHandlePersistence.CountNotFlowCensorshipIdByWorkflowIdAndHandleStatus(findFlowCensorshipIn.Workflow.Id, 
                    HandleStatusEnum.UN_HANDLE, findFlowCensorshipIn.CurrWorkflowHandle.FlowCensorshipId, connectionId);
                if (existsOtherUnHandleCount > 0)
                {
                    return;
                }

                // 查找下一个送件流程关卡
                FlowCensorshipInfo[] nextSendFlowCensorships = FindSendNextFlowCensorships(returnInfo, findFlowCensorshipIn, findFlowCensorshipIn.CurrWorkflowHandle.FlowCensorshipId);
                if (nextSendFlowCensorships.IsNullOrLength0())
                {
                    returnInfo.SetFailureMsg("找不到下一个处理流程关卡");
                    return;
                }

                // 结束的标准关卡
                StandardCensorshipInfo endStand = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.END);
                if (endStand == null)
                {
                    returnInfo.SetFailureMsg("找不到结束的标准关卡");
                    return;
                }

                // 如果下一个关卡为结束，则直接通知申请者  
                foreach (var f in nextSendFlowCensorships)
                {
                    if (f.OwnerCensorshipType == CensorshipTypeEnum.STANDARD && f.OwnerCensorshipId == endStand.Id)
                    {
                        // 查找结束流程关卡ID
                        FlowCensorshipInfo applyFlowCensors = FindFlowCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.END);
                        StandardCensorshipInfo applyStandCensors = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.END);
                        ConcreteCensorshipInfo applyConsors = new ConcreteCensorshipInfo()
                        {
                            Id = applyFlowCensors.Id,
                            Code = applyStandCensors.Code,
                            Name = applyStandCensors.Name,
                            WorkflowHandles = new WorkflowHandleInfo[]
                            {
                                new WorkflowHandleInfo()
                                {
                                    ConcreteConcrete = applyStandCensors.Name,
                                    ConcreteConcreteId = applyStandCensors.Id,
                                    FlowCensorshipId = applyFlowCensors.Id,
                                    Handler = findFlowCensorshipIn.Workflow.Creater,
                                    HandlerId = findFlowCensorshipIn.Workflow.CreaterId,
                                    HandleStatus = HandleStatusEnum.UN_HANDLE,
                                    HandleType = HandleTypeEnum.NOTIFY
                                }
                            },
                            FlowCensorship = applyFlowCensors
                        };

                        applyConsors.WorkflowHandles[0].SetCreateInfo(currUser);

                        returnInfo.Data.Workflow.FlowStatus = FlowStatusEnum.AUDIT_PASS;
                        returnInfo.Data.Workflow.SetModifyInfo(currUser);

                        ConcreteCensorshipInfo[] nextConcreteCens = new ConcreteCensorshipInfo[] { applyConsors };
                        UpdateCurrWorkflowHandleInfo(nextConcreteCens, returnInfo.Data.Workflow);

                        returnInfo.Data.NextConcreteCensorshipHandles = nextConcreteCens;

                        return;
                    }
                }

                // 查找具体的送件流程关卡
                ConcreteCensorshipInfo[] concreteCensorships = FindMappingConcreteCensorships(returnInfo, findFlowCensorshipIn, nextSendFlowCensorships, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return;
                }

                UpdateCurrWorkflowHandleInfo(concreteCensorships, returnInfo.Data.Workflow);

                returnInfo.Data.NextConcreteCensorshipHandles = concreteCensorships;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(idea))
                {
                    idea = "退件";
                }
                findFlowCensorshipIn.CurrWorkflowHandle.Idea = idea;

                // 查找下一个退件流程关卡
                FlowCensorshipInfo[] nextReturnFlowCensorships = FindReturnNextFlowCensorships(returnInfo, findFlowCensorshipIn, findFlowCensorshipIn.CurrWorkflowHandle.FlowCensorshipId);
                if (nextReturnFlowCensorships.IsNullOrLength0())
                {
                    returnInfo.SetFailureMsg("找不到下一个处理流程关卡");
                    return;
                }

                // 申请者的标准关卡
                StandardCensorshipInfo applyStand = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);
                if (applyStand == null)
                {
                    returnInfo.SetFailureMsg("找不到申请者的标准关卡");
                    return;
                }

                findFlowCensorshipIn.CurrWorkflowHandle.HandleStatus = HandleStatusEnum.RETURNED;

                // 如果下一个关卡为申请者，则直接通知申请者
                foreach (var f in nextReturnFlowCensorships)
                {
                    if (f.OwnerCensorshipType == CensorshipTypeEnum.STANDARD && applyStand.Id == f.OwnerCensorshipId)
                    {
                        // 查找申请者流程关卡ID
                        FlowCensorshipInfo applyFlowCensors = FindFlowCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);
                        StandardCensorshipInfo applyStandCensors = FindStandardCensorshipByStandardCode(findFlowCensorshipIn, StandardCensorshipDefine.APPLICANT);
                        ConcreteCensorshipInfo applyConsors = new ConcreteCensorshipInfo()
                        {
                            Id = applyFlowCensors.Id,
                            Code = applyStandCensors.Code,
                            Name = applyStandCensors.Name,
                            WorkflowHandles = new WorkflowHandleInfo[]
                            {
                                new WorkflowHandleInfo()
                                {
                                    ConcreteConcrete = applyStandCensors.Name,
                                    ConcreteConcreteId = applyStandCensors.Id,
                                    FlowCensorshipId = applyFlowCensors.Id,
                                    Handler = findFlowCensorshipIn.Workflow.Creater,
                                    HandlerId = findFlowCensorshipIn.Workflow.CreaterId,
                                    HandleStatus = HandleStatusEnum.UN_HANDLE,
                                    HandleType = HandleTypeEnum.NOTIFY
                                }
                            },
                            FlowCensorship = applyFlowCensors
                        };

                        applyConsors.WorkflowHandles[0].SetCreateInfo(currUser);

                        returnInfo.Data.Workflow.FlowStatus = FlowStatusEnum.AUDIT_NOPASS;
                        returnInfo.Data.Workflow.SetModifyInfo(currUser);

                        var nextConcreteCens = new ConcreteCensorshipInfo[] { applyConsors };
                        UpdateCurrWorkflowHandleInfo(nextConcreteCens, returnInfo.Data.Workflow);

                        returnInfo.Data.NextConcreteCensorshipHandles = nextConcreteCens;

                        return;
                    }
                }

                int[] nextFlowCIds = new int[nextReturnFlowCensorships.Length];
                for (var i = 0; i < nextFlowCIds.Length; i++)
                {
                    nextFlowCIds[i] = nextReturnFlowCensorships[i].Id;
                }
                // 退件直接找从已处理列表中的各流程关卡的处理人
                IList<WorkflowHandleInfo> returnWorkflowHandles = WorkflowHandlePersistence.SelectSendedByWorkflowIdAndFlowCensorshipIds(findFlowCensorshipIn.Workflow.Id, nextFlowCIds, connectionId);
                if (returnWorkflowHandles.IsNullOrCount0())
                {
                    returnInfo.SetFailureMsg("找不到可退件的人");
                    return;
                }

                List<ConcreteCensorshipInfo> conList = new List<ConcreteCensorshipInfo>();
                // 具体ID映射工作流处理列表
                IDictionary<int, IList<WorkflowHandleInfo>> dicConIdMapHandles = new Dictionary<int, IList<WorkflowHandleInfo>>();

                // 处理人ID列表
                IList<int> handleIds = new List<int>();

                foreach (var w in returnWorkflowHandles)
                {
                    // 过滤重复的处理人ID
                    if (handleIds.Contains(w.HandlerId))
                    {
                        continue;
                    }
                    handleIds.Add(w.HandlerId);

                    WorkflowHandleInfo newHandle = w.Clone() as WorkflowHandleInfo;
                    newHandle.HandleStatus = HandleStatusEnum.UN_HANDLE;
                    newHandle.HandleTime = null;
                    newHandle.Idea = null;
                    newHandle.IsReaded = false;

                    var c = conList.Find(x => x.Id == w.ConcreteConcreteId);
                    if (c == null)
                    {
                        c = new ConcreteCensorshipInfo()
                        {
                            Id = w.ConcreteConcreteId,
                            Name = w.ConcreteConcrete,
                            FlowCensorship = new FlowCensorshipInfo()
                            {
                                Id = w.FlowCensorshipId
                            }
                        };

                        dicConIdMapHandles.Add(c.Id, new List<WorkflowHandleInfo>() { newHandle });

                        conList.Add(c);
                    }
                    else
                    {
                        dicConIdMapHandles[c.Id].Add(newHandle);
                    }                    
                }

                foreach (var c in conList)
                {
                    foreach (KeyValuePair<int, IList<WorkflowHandleInfo>> item in dicConIdMapHandles)
                    {
                        if (c.Id == item.Key)
                        {
                            c.WorkflowHandles = item.Value.ToArray();

                            break;
                        }
                    }
                }

                ConcreteCensorshipInfo[] cons = conList.ToArray();
                UpdateCurrWorkflowHandleInfo(cons, returnInfo.Data.Workflow);

                returnInfo.Data.NextConcreteCensorshipHandles = cons;
            }
        }

        /// <summary>
        /// 根据当前流程关卡ID查找下一个送件流程关卡数组
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="currFlowCensorshipId">当前流程关卡ID</param>
        /// <returns>下一个送件流程关卡数组</returns>
        private FlowCensorshipInfo[] FindSendNextFlowCensorships(ReturnInfo<FlowCensorshipOutInfo> returnInfo, FlowCensorshipInInfo findFlowCensorshipIn, int currFlowCensorshipId)
        {
            // 查找当前流程关卡ID的送件路线流转到下一个流程关卡ID
            IList<int> nextIds = new List<int>();
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (fc.SendFlowRoutes.IsNullOrLength0())
                {
                    continue;
                }

                foreach (var s in fc.SendFlowRoutes)
                {
                    if (s.FlowCensorshipId == currFlowCensorshipId)
                    {
                        nextIds.Add(s.ToFlowCensorshipId);
                    }
                }
            }

            if (nextIds.Count == 0)
            {
                return null;
            }

            return FindFlowCensorshipsByFlowCensorshipIds(findFlowCensorshipIn, nextIds.ToArray());
        }

        /// <summary>
        /// 根据当前流程关卡ID查找下一个退件流程关卡数组
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="currFlowCensorshipId">当前流程关卡ID</param>
        /// <returns>下一个退件流程关卡数组</returns>
        private FlowCensorshipInfo[] FindReturnNextFlowCensorships(ReturnInfo<FlowCensorshipOutInfo> returnInfo, FlowCensorshipInInfo findFlowCensorshipIn, int currFlowCensorshipId)
        {
            // 查找当前流程关卡ID的送件路线流转到下一个流程关卡ID
            IList<int> nextIds = new List<int>();
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (fc.ReturnFlowRoutes.IsNullOrLength0())
                {
                    continue;
                }

                foreach (var s in fc.ReturnFlowRoutes)
                {
                    if (s.FlowCensorshipId == currFlowCensorshipId)
                    {
                        nextIds.Add(s.ToFlowCensorshipId);
                    }
                }
            }

            if (nextIds.Count == 0)
            {
                return null;
            }

            return FindFlowCensorshipsByFlowCensorshipIds(findFlowCensorshipIn, nextIds.ToArray());
        }

        /// <summary>
        /// 根据标准关卡编码查找标准流程关卡
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="standardCode">标准关卡编码</param>
        /// <returns>标准流程关卡</returns>
        private StandardCensorshipInfo FindStandardCensorshipByStandardCode(FlowCensorshipInInfo findFlowCensorshipIn, string standardCode)
        {
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (fc.OwnerCensorshipType == CensorshipTypeEnum.STANDARD)
                {
                    foreach (var s in fc.StandardCensorships)
                    {
                        if (s.Code == standardCode)
                        {
                            return s;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据标准关卡编码查找流程关卡
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="standardCode">标准关卡编码</param>
        /// <returns>流程关卡</returns>
        private FlowCensorshipInfo FindFlowCensorshipByStandardCode(FlowCensorshipInInfo findFlowCensorshipIn, string standardCode)
        {
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (fc.OwnerCensorshipType == CensorshipTypeEnum.STANDARD)
                {
                    foreach (var s in fc.StandardCensorships)
                    {
                        if (s.Code == standardCode)
                        {
                            return fc;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据标准关卡ID查找标准流程关卡
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="standardId">标准关卡ID</param>
        /// <returns>标准流程关卡</returns>
        private StandardCensorshipInfo FindStandardCensorshipByStandardId(FlowCensorshipInInfo findFlowCensorshipIn, int standardId)
        {
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (fc.OwnerCensorshipType == CensorshipTypeEnum.STANDARD)
                {
                    foreach (var s in fc.StandardCensorships)
                    {
                        if (s.Id == standardId)
                        {
                            return s;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据流程关卡ID数组查找流程关卡数组
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="flowCensorshipIds">流程关卡ID数组</param>
        /// <returns>流程关卡数组</returns>
        private FlowCensorshipInfo[] FindFlowCensorshipsByFlowCensorshipIds(FlowCensorshipInInfo findFlowCensorshipIn, int[] flowCensorshipIds)
        {
            IList<FlowCensorshipInfo> result = new List<FlowCensorshipInfo>();
            foreach (var id in flowCensorshipIds)
            {
                foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
                {
                    if (id == fc.Id)
                    {
                        result.Add(fc);
                        break;
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 根据流程关卡ID查找流程关卡
        /// </summary>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <returns>流程关卡</returns>
        private FlowCensorshipInfo FindFlowCensorshipByFlowCensorshipId(FlowCensorshipInInfo findFlowCensorshipIn, int flowCensorshipId)
        {
            foreach (var fc in findFlowCensorshipIn.WorkflowDefine.Flow.FlowCensorships)
            {
                if (flowCensorshipId == fc.Id)
                {
                    return fc;
                }
            }

            return null;
        }

        /// <summary>
        /// 查找映射对应的具体关卡数组
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="flowCensorships">流程关卡数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>具体关卡数组</returns>
        private ConcreteCensorshipInfo[] FindMappingConcreteCensorships(ReturnInfo<FlowCensorshipOutInfo> returnInfo, FlowCensorshipInInfo findFlowCensorshipIn, FlowCensorshipInfo[] flowCensorships, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            IList<ConcreteCensorshipInfo> concreteCensorships = new List<ConcreteCensorshipInfo>();

            var user = UserTool<int>.GetCurrUser(currUser);
            ExecProcConnectionId(connId =>
            {
                foreach (var f in flowCensorships)
                {
                    switch (f.OwnerCensorshipType)
                    {
                        case CensorshipTypeEnum.ROLE:
                            ExecGetToUsers(returnInfo, findFlowCensorshipIn, f, concreteCensorships,
                            () =>
                            {
                                return FindHandlerRoleUser.FindById(f.OwnerCensorshipId, user.Id, connId, currUser);
                            }, currUser);
                            if (returnInfo.Failure())
                            {
                                return;
                            }
                            break;

                        case CensorshipTypeEnum.USER:
                            ExecGetToUsers(returnInfo, findFlowCensorshipIn, f, concreteCensorships,
                            () =>
                            {
                                return FindHandlerConcreteUser.FindById(f.OwnerCensorshipId, user.Id, connId, currUser);
                            }, currUser);
                            if (returnInfo.Failure())
                            {
                                return;
                            }

                            break;

                        case CensorshipTypeEnum.STANDARD:
                            StandardCensorshipInfo standardCensorship = FindStandardCensorshipByStandardId(findFlowCensorshipIn, f.OwnerCensorshipId);
                            if (standardCensorship != null)
                            {
                                if (standardCensorship.Code == StandardCensorshipDefine.SUPERVISOR)
                                {
                                    ExecGetToUsers(returnInfo, findFlowCensorshipIn, f, concreteCensorships,
                                        () =>
                                        {
                                            return FindHandlerSupervisorUser.FindById(f.OwnerCensorshipId, user.Id, connId, currUser);
                                        }, currUser);
                                    if (returnInfo.Failure())
                                    {
                                        return;
                                    }
                                }
                            }

                            break;
                    }
                }
            }, connectionId, AccessMode.SLAVE);
            if (returnInfo.Failure())
            {
                return null;
            }
            
            if (concreteCensorships.Count == 0)
            {
                returnInfo.SetFailureMsg("找不到下一个处理者");
                return null;
            }

            return concreteCensorships.ToArray();
        }

        /// <summary>
        /// 执行获取到用户数组
        /// 且构造处理者
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入</param>
        /// <param name="flowCensorship">流程关卡</param>
        /// <param name="concreteCensorships">具体关卡列表</param>
        /// <param name="funcToUsers">回调获取到用户数组</param>
        /// <param name="currUser">当前用户</param>
        private void ExecGetToUsers(ReturnInfo<FlowCensorshipOutInfo> returnInfo,
            FlowCensorshipInInfo findFlowCensorshipIn,
            FlowCensorshipInfo flowCensorship,
            IList<ConcreteCensorshipInfo> concreteCensorships,
            Func<ReturnInfo<FindHandlerUserOutInfo>> funcToUsers, 
            BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<FindHandlerUserOutInfo> reUsers = funcToUsers();
            if (reUsers.Failure())
            {
                returnInfo.FromBasic(reUsers);
                return;
            }
            if (reUsers.Data == null || reUsers.Data.Users.IsNullOrLength0())
            {
                return;
            }

            IList<WorkflowHandleInfo> workflowHandles = new List<WorkflowHandleInfo>(reUsers.Data.Users.Length);
            foreach (var u in reUsers.Data.Users)
            {
                bool isExists = false;

                // 查找是否已经存在处理者
                foreach (var fc in concreteCensorships)
                {
                    if (fc.WorkflowHandles.IsNullOrLength0())
                    {
                        continue;
                    }
                    foreach (var h in fc.WorkflowHandles)
                    {
                        if (h.HandlerId == u.Id)
                        {
                            isExists = true;
                            break;
                        }
                    }
                    if (isExists)
                    {
                        break;
                    }
                }

                if (isExists)
                {
                    continue;
                }

                // 构造处理者数组
                WorkflowHandleInfo wh = new WorkflowHandleInfo()
                {
                    ConcreteConcrete = reUsers.Data.ConcreteCensorship.Name,
                    ConcreteConcreteId = reUsers.Data.ConcreteCensorship.Id,
                    FlowCensorshipId = flowCensorship.Id,
                    Handler = u.Name,
                    HandlerId = u.Id,
                    HandleType = HandleTypeEnum.AUDIT,
                    HandleStatus = HandleStatusEnum.UN_HANDLE,
                };
                if (findFlowCensorshipIn.Workflow != null)
                {
                    wh.WorkflowId = findFlowCensorshipIn.Workflow.Id;
                }
                wh.SetCreateInfo(currUser);

                workflowHandles.Add(wh);
            }

            if (workflowHandles.Count == 0)
            {
                return;
            }

            concreteCensorships.Add(new ConcreteCensorshipInfo()
            {
                Id = reUsers.Data.ConcreteCensorship.Id,
                Code = reUsers.Data.ConcreteCensorship.Code,
                Name = reUsers.Data.ConcreteCensorship.Name,
                WorkflowHandles = workflowHandles.ToArray(),
                FlowCensorship = flowCensorship
            });
        }

        /// <summary>
        /// 更新当前工作流处理信息
        /// </summary>
        /// <param name="concreteCensorships">具体关卡数组</param>
        /// <param name="workflow">工作流</param>
        private void UpdateCurrWorkflowHandleInfo(ConcreteCensorshipInfo[] concreteCensorships, WorkflowInfo workflow)
        {
            StringBuilder currFlowCensorshipIds = new StringBuilder();
            StringBuilder currConcreteCensorshipIds = new StringBuilder();
            StringBuilder currConcreteCensorships = new StringBuilder();
            StringBuilder currHandlerIds = new StringBuilder();
            StringBuilder currHandlers = new StringBuilder();

            foreach (var c in concreteCensorships)
            {
                currFlowCensorshipIds.AppendFormat("{0},", c.FlowCensorship.Id);
                currConcreteCensorshipIds.AppendFormat("{0},", c.Id);
                currConcreteCensorships.AppendFormat("{0},", c.Name);

                foreach (var h in c.WorkflowHandles)
                {
                    currHandlerIds.AppendFormat("{0},", h.HandlerId);
                    currHandlers.AppendFormat("{0},", h.Handler);
                }
            }

            workflow.CurrConcreteCensorshipIds = currConcreteCensorshipIds.ToString().TrimEnd(',');
            workflow.CurrConcreteCensorships = currConcreteCensorships.ToString().TrimEnd(',');
            workflow.CurrHandlerIds = currHandlerIds.ToString().TrimEnd(',');
            workflow.CurrHandlers = currHandlers.ToString().TrimEnd(',');
            workflow.CurrFlowCensorshipIds = currFlowCensorshipIds.ToString().TrimEnd(',');
        }

        #endregion
    }
}
