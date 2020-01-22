using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Impl.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.WorkFlow.Standard
{
    /// <summary>
    /// 工作流初始序列服务
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FormT">表单类型</typeparam>
    [Inject]
    public class WorkflowInitSequenceService : WorkflowFormService
    {
        /// <summary>
        /// 序列服务
        /// </summary>
        public ISequenceService SequenceService
        {
            get;
            set;
        }

        /// <summary>
        /// 生成申请单号
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始化</param>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>申请单号</returns>
        protected override string BuilderApplyNo<FormT>(FlowInitInfo<FormT> flowInit, ReturnInfo<WorkflowBasicInfo> returnInfo)
        {
            var buildNoReturnInfo = SequenceService.BuildNo(flowInit.WorkflowCode);
            if (buildNoReturnInfo.Failure())
            {
                returnInfo.FromBasic(buildNoReturnInfo);

                return null;
            }

            return buildNoReturnInfo.Data;
        }
    }
}
