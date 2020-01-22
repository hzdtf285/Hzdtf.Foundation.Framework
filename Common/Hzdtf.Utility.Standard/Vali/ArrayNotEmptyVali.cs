using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 数组不为空验证
    /// @ 黄振东
    /// </summary>
    public class ArrayNotEmptyVali : IValiHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="paramIndex">参数索引位置</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo Exec(object[] param, byte paramIndex = 0)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();
            if (param[paramIndex] == null)
            {
                returnInfo.SetFailureMsg("数组不能为null");
                return returnInfo;
            }

            if (param[paramIndex] is Array)
            {
                Array array = param[paramIndex] as Array;
                if (array.Length == 0)
                {
                    returnInfo.SetFailureMsg("数组长度必须大于0");
                    return returnInfo;
                }
            }
            else
            {
                returnInfo.SetFailureMsg("对象不是数组");
            }

            return returnInfo;
        }
    }
}
