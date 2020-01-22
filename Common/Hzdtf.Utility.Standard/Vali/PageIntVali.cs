using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 分页值验证
    /// @ 黄振东
    /// </summary>
    public class PageIntVali : IValiHandler
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
            if (Convert.ToInt32(param[paramIndex]) < 0)
            {
                returnInfo.SetFailureMsg("页码必须大于或等于0");
                return returnInfo;
            }

            int pageSize = Convert.ToInt32(param[paramIndex + 1]);
            if (pageSize < 1)
            {
                returnInfo.SetFailureMsg("每页记录数必须大于0");
                return returnInfo;
            }
            if (UtilTool.MaxPageSize != -1 && pageSize > UtilTool.MaxPageSize)
            {
                returnInfo.SetFailureMsg($"每页记录数已经超过了最大记录数:{UtilTool.MaxPageSize}");
                return returnInfo;
            }

            return returnInfo;
        }
    }
}
