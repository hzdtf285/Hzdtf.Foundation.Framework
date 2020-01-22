using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Impl.Standard.TypeMapper
{
    /// <summary>
    /// Sql Server映射服务
    /// @ 黄振东
    /// </summary>
    public class SqlServerMapperService : TypeMapperBase
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
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                case "uniqueidentifier":
                    return "string";
                    
                case "int":
                    isValueType = true;
                    return "int";

                case "smallint":
                    isValueType = true;
                    return "short";

                case "datetime":
                case "datetime2":
                case "date":
                case "timestamp":
                case "time":
                case "smalldatetime":
                case "datetimeoffset":
                    isValueType = true;
                    return "DateTime";

                case "bigint":
                    isValueType = true;
                    return "long";

                case "tinyint":
                    isValueType = true;
                    return "byte";

                case "bit":
                    isValueType = true;
                    return "bool";

                case "real":
                case "float":
                    isValueType = true;
                    return "float";

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    isValueType = true;
                    return "decimal";

                default:
                    return "object";
            }
        }
    }
}
