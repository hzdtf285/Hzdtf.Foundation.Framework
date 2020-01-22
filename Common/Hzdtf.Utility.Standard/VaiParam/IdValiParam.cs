using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// ID验证参数
    /// @ 黄振东
    /// </summary>
    public class IdValiParam : ValiParamBase<IdAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, IdAttribute valiAttr, string displayName)
        {
            string name = string.IsNullOrWhiteSpace(displayName) ? "Id" : displayName;
            if (value == null)
            {
                return null;
            }
            if (Convert.ToUInt32(value) <= 0)
            {
                return $"{name}必须大于0";
            }

            return null;
        }
    }
}
