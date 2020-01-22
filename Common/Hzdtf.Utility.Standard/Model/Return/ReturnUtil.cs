using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 返回辅助类
    /// @ 黄振东
    /// </summary>
    public static class ReturnUtil
    {
        /// <summary>
        /// 判断类型是否为返回类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型是否为本类</returns>
        public static bool IsReturnType(this Type type)
        {
            return type != null ? type.FullName.StartsWith(ReturnInfo<DBNull>.BASIC_FULL_NAME) 
                || BasicReturnInfo.BASIC_FULL_NAME.Equals(type.FullName)
                || DefaultReturnInfo.BASIC_FULL_NAME.Equals(type.FullName)
                || type.FullName.StartsWith(Page1ReturnInfo<DBNull>.BASIC_FULL_NAME)
                : false;
        }
    }
}
