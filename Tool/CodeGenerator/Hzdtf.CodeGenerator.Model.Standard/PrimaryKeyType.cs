using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 主键类型
    /// @ 黄振东
    /// </summary>
    public enum PrimaryKeyType : byte
    {
        /// <summary>
        /// 整型
        /// </summary>
        INT = 0,

        /// <summary>
        /// 字符串
        /// </summary>
        STRING = 1,

        /// <summary>
        /// GUID
        /// </summary>
        GUID = 2,

        /// <summary>
        /// 雪花算法
        /// </summary>
        SNOWFLAKE = 3
    }

    /// <summary>
    /// 主键类型扩展类
    /// @ 黄振东
    /// </summary>
    public static class PrimaryKeyExtensions
    {
        /// <summary>
        /// 根据主键类型转换为编码字符串
        /// </summary>
        /// <param name="pkType">主键类型</param>
        /// <returns>编码字符串</returns>
        public static string ToCodeString(this PrimaryKeyType pkType)
        {
            switch (pkType)
            {
                case PrimaryKeyType.INT:

                    return "int";

                case PrimaryKeyType.STRING:

                    return "string";

                case PrimaryKeyType.GUID:

                    return "Guid";

                case PrimaryKeyType.SNOWFLAKE:

                    return "long";

                default:

                    return null;
            }
        }
    }
}
