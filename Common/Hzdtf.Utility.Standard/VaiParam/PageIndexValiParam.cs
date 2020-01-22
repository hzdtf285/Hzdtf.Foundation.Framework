using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 页码验证参数
    /// @ 黄振东
    /// </summary>
    public class PageIndexValiParam : ValiParamBase<PageIndexAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, PageIndexAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }
            if (Convert.ToInt32(value) < 0)
            {
                return "页码必须大于或等于0";
            }

            return null;
        }
    }
}
