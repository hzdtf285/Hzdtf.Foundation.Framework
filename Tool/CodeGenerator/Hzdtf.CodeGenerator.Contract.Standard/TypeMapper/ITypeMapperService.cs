using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Contract.Standard.TypeMapper
{
    /// <summary>
    /// 类型映射服务接口
    /// @ 黄振东
    /// </summary>
    public interface ITypeMapperService
    {
        /// <summary>
        /// 根据列信息获取属性类型
        /// </summary>
        /// <param name="column">列信息</param>
        /// <returns>属性类型</returns>
        string GetPropertyType(ColumnInfo column);
    }
}
