using Hzdtf.Utility.Standard.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 流程状态转换
    /// @ 黄振东
    /// </summary>
    public class FlowStatusConvert : ConvertBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value)
        {
            FlowStatusEnum typeValue = (FlowStatusEnum)value;
            switch (typeValue)
            {
                case FlowStatusEnum.DRAFT:
                    return "草稿";

                case FlowStatusEnum.AUDITING:
                    return "审核中";

                case FlowStatusEnum.AUDIT_PASS:
                    return "审核通过";

                case FlowStatusEnum.AUDIT_NOPASS:
                    return "审核驳回";

                case FlowStatusEnum.REVERSED:
                    return "已撤消";

                default:
                    return null;
            }
        }
    }
}
