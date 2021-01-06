using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Contract.Standard.Function
{
    /// <summary>
    /// 功能生成服务接口
    /// @ 黄振东
    /// </summary>
    public interface IFunctionGeneratorService
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Generator(CodeGeneratorParamInfo param);
    }
}
