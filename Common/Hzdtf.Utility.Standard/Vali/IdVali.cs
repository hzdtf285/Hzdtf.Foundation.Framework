using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// ID验证
    /// @ 黄振东
    /// </summary>
    public class IdVali : IValiHandler
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
                returnInfo.SetFailureMsg("ID不能为null");
                return returnInfo;
            }
            if (Convert.ToUInt32(param[paramIndex]) <= 0)
            {
                returnInfo.SetFailureMsg("ID必须大于0");
                return returnInfo;
            }

            return returnInfo;
        }
    }
}
