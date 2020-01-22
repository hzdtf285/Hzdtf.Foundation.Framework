using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 每页记录数验证参数
    /// @ 黄振东
    /// </summary>
    public class PageSizeValiParam : ValiParamBase<PageSizeAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, PageSizeAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }

            int pageSize = Convert.ToInt32(value);
            if (pageSize < 1)
            {
                return "每页记录数必须大于0";
            }
            if (valiAttr.MaxPageSize != -1 && pageSize > valiAttr.MaxPageSize)
            {
                return $"每页记录数已经超过了最大记录数:{valiAttr.MaxPageSize}";
            }

            return null;
        }
    }
}
