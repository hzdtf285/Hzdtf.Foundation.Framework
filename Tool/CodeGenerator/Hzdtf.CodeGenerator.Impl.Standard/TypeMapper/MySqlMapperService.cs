using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Impl.Standard.TypeMapper
{
    /// <summary>
    /// MySql映射服务
    /// @ 黄振东
    /// </summary>
    public class MySqlMapperService : TypeMapperBase
    {
        /// <summary>
        /// 根据数据类型获取属性类型
        /// </summary>
        /// <param name="column">列信息</param>
        /// <param name="isValueType">是否值类型</param>
        /// <returns>属性类型</returns>
        protected override string GetPropertyTypeByDataType(ColumnInfo column, out bool isValueType)
        {
            isValueType = false;
            string colType = column.DataType.ToLower();
            switch (colType)
            {
                case "varchar":
                case "char":
                case "text":
                case "longtext":
                case "tinytext":
                    return "string";
                    
                case "int":
                    isValueType = true;
                    return "int";

                case "smallint":
                    isValueType = true;
                    return "short";

                case "datetime":
                case "date":
                case "timestamp":
                    isValueType = true;
                    return "DateTime";

                case "bigint":
                    isValueType = true;
                    return "long";

                case "tinyint":
                    isValueType = true;

                    if ((column.ColumnType != null && "tinyint(1)".Equals(column.ColumnType.ToLower()))
                        || (column.Length != null && column.Length == 1))
                    {
                        return "bool";
                    }
                    
                    return "int";

                case "boolean":
                case "bool":
                case "bit":
                    isValueType = true;
                    return "bool";
                    
                case "double":
                    isValueType = true;
                    return "double";

                case "decimal":
                    isValueType = true;
                    return "decimal";

                case "real":
                case "float":
                    isValueType = true;
                    return "float";

                case "numeric":
                    isValueType = true;
                    return "decimal";

                default:
                    return "object";
            }
        }
    }
}
