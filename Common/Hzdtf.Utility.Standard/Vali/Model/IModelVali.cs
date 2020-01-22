using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali.Model
{
    /// <summary>
    /// 模型验证
    /// @ 黄振东
    /// </summary>
    public interface IModelVali
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        BasicReturnInfo Vali(object model);
    }
}
