using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 数组不为空验证参数
    /// @ 黄振东
    /// </summary>
    public class ArrayNotEmptyValiParam : ValiParamBase<ArrayNotEmptyAttribute>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, ArrayNotEmptyAttribute valiAttr, string displayName)
        {
            string name = string.IsNullOrWhiteSpace(displayName) ? "数组" : displayName;
            if (value == null)
            {
                return $"{name}不能为null";
            }

            if (value is Array)
            {
                Array array = value as Array;
                if (array.Length == 0)
                {
                    return $"{name}长度必须大于0";
                }
            }
            else
            {
                return $"{name}不是非数组格式";
            }

            return null;
        }
    }
}
