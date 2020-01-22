using Hzdtf.Utility.Standard.Attr;
using Hzdtf.WorkFlow.Model.Standard.Expand;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Impl.Standard.Engine
{
    /// <summary>
    /// 工作流强制移除
    /// </summary>
    [Inject]
    public class WorkflowForceRemove : WorkflowRemoveBase, IWorkflowForceRemove
    {
        /// <summary>
        /// 获取移除类型
        /// </summary>
        /// <returns>移除类型</returns>
        protected override RemoveType GetRemoveType() => RemoveType.FORCE_REMOVE;
    }
}
