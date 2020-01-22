using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 验证处理接口
    /// @ 黄振东
    /// </summary>
    public interface IValiHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="paramIndex">模型参数索引位置</param>
        /// <returns>返回信息</returns>
        BasicReturnInfo Exec(object[] param, byte paramIndex = 0);
    }
}
