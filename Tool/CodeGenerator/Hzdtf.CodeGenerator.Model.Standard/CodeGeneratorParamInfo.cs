using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 代码参数信息
    /// @ 黄振东
    /// </summary>
    public class CodeParamInfo
    {
        /// <summary>
        /// 功能类型数组
        /// </summary>
        public FunctionType[] FunctionTypes
        {
            get;
            set;
        }

        /// <summary>
        /// 命名空间前辍
        /// </summary>
        public string NamespacePfx
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 主键类型
        /// </summary>
        public PrimaryKeyType PkType
        {
            get;
            set;
        } = PrimaryKeyType.INT;

        /// <summary>
        /// 是否租户
        /// </summary>
        public bool IsTenant
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 代码生成参数信息
    /// @ 黄振东
    /// </summary>
    public class CodeGeneratorParamInfo : CodeParamInfo
    {
        /// <summary>
        /// 表列表
        /// </summary>
        public IList<TableInfo> Tables
        {
            get;
            set;
        }
    }
}
