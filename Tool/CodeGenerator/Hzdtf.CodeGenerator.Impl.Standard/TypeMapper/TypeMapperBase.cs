using Hzdtf.CodeGenerator.Contract.Standard.TypeMapper;
using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Impl.Standard.TypeMapper
{
    /// <summary>
    /// 类型映射基类
    /// @ 黄振东
    /// </summary>
    public abstract class TypeMapperBase : ITypeMapperService
    {
        /// <summary>
        /// 根据列信息获取属性类型
        /// </summary>
        /// <param name="column">列信息</param>
        /// <returns>属性类型</returns>
        public string GetPropertyType(ColumnInfo column)
        {
            bool isValueTpe;
            string propType = GetPropertyTypeByDataType(column, out isValueTpe);
            if (isValueTpe && column.IsNull)
            {
                propType += "?";
            }

            return propType;
        }

        /// <summary>
        /// 根据数据类型获取属性类型
        /// </summary>
        /// <param name="column">列信息</param>
        /// <param name="isValueType">是否值类型</param>
        /// <returns>属性类型</returns>
        protected abstract string GetPropertyTypeByDataType(ColumnInfo column, out bool isValueType);
    }
}
