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
        /// <param name="tables">表信息列表</param>
        /// <param name="namespacePfx">命名空间前辍</param>
        /// <param name="type">类型</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Generator(IList<TableInfo> tables, string namespacePfx, string type);
    }
}
