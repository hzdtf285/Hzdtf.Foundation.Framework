using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// 持久生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class PersistenceGeneratorService : FunctionGeneratorBase
    {
        /// <summary>
        /// 接口模板
        /// </summary>
        private static string interfaceTemplate;

        /// <summary>
        /// 接口模板
        /// </summary>
        private static string InterfaceTemplate
        {
            get
            {
                if (interfaceTemplate == null)
                {
                    interfaceTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Persistence\\interfaceTemplate.txt".ReaderFileContent();
                }

                return interfaceTemplate;
            }
        }

        /// <summary>
        /// MySql类模板
        /// </summary>
        private static string mySqlClassTemplate;

        /// <summary>
        /// MySql类模板
        /// </summary>
        private static string MySqlClassTemplate
        {
            get
            {
                if (mySqlClassTemplate == null)
                {
                    mySqlClassTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Persistence\\Impl\\mysqlClassTemplate.txt".ReaderFileContent();
                }

                return mySqlClassTemplate;
            }
        }

        /// <summary>
        /// SqlServer类模板
        /// </summary>
        private static string sqlSqlClassTemplate;

        /// <summary>
        /// SqlServer类模板
        /// </summary>
        private static string SqlServerClassTemplate
        {
            get
            {
                if (sqlSqlClassTemplate == null)
                {
                    sqlSqlClassTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Persistence\\Impl\\sqlserverClassTemplate.txt".ReaderFileContent();
                }

                return sqlSqlClassTemplate;
            }
        }

        /// <summary>
        /// 获取值分支模板
        /// </summary>
        private static string getValueCaseTemplate;

        /// <summary>
        /// 获取值分支模板
        /// </summary>
        private static string GetValueCaseTemplate
        {
            get
            {
                if (getValueCaseTemplate == null)
                {
                    getValueCaseTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Persistence\\Impl\\getValueCaseTemplate.txt".ReaderFileContent();
                }

                return getValueCaseTemplate;
            }
        }

        /// <summary>
        /// 忽略的插入属性集合
        /// </summary>
        private static readonly string[] IGNORE_INSERT_PROPS = new string[]
        {
        };

        /// <summary>
        /// 忽略的修改属性集合
        /// </summary>
        private static readonly string[] IGNORE_UPDATE_PROPS = new string[]
        {
            "Id", "CreaterId", "Creater", "CreateTime"
        };

        /// <summary>
        /// 生成代码文本集合
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="codeParam">代码参数</param>
        /// <param name="fileNames">文件名集合</param>
        /// <returns>代码文本集合</returns>
        protected override string[] BuilderCodeTexts(TableInfo table, CodeParamInfo codeParam, out string[] fileNames)
        {
            string interfaceFile, classFile;
            string interfaceCode = BuilderInterfaceCode(table, codeParam, out interfaceFile);
            string classCode = BuilderClassCode(table, codeParam, out classFile);

            fileNames = new string[] { interfaceFile, classFile };
            return new string[] { interfaceCode, classCode };
        }

        /// <summary>
        /// 生成接口代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="codeParam">代码参数</param>
        /// <param name="fileName">文件名</param>
        /// <returns>接口代码</returns>
        private string BuilderInterfaceCode(TableInfo table, CodeParamInfo codeParam, out string fileName)
        {
            string basicName = table.Name.FristUpper();
            string name = $"I{basicName}Persistence";
            fileName = $"{name}.cs";

            var desc = string.IsNullOrWhiteSpace(table.Description) ? basicName : table.Description;
            return InterfaceTemplate
                .Replace("|NamespacePfx|", codeParam.NamespacePfx)
                .Replace("|Description|", desc)
                .Replace("|Name|", name)
                .Replace("|Model|", basicName)
                .Replace("|PkType|", codeParam.PkType.ToCodeString());
        }

        /// <summary>
        /// 生成类代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="codeParam">代码参数</param>
        /// <param name="fileName">文件名</param>
        /// <returns>类代码</returns>
        private string BuilderClassCode(TableInfo table, CodeParamInfo codeParam, out string fileName)
        {
            string basicName = table.Name.FristUpper();
            string name = $"{basicName}Persistence";
            fileName = $"{name}.cs";

            StringBuilder insFiled = new StringBuilder();
            StringBuilder updFiled = new StringBuilder();
            StringBuilder selAllFiled = new StringBuilder();
            StringBuilder getValueCase = new StringBuilder();

            if (!table.Columns.IsNullOrCount0())
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    ColumnInfo c = table.Columns[i];
                    string propName = c.Name.FristUpper();
                    if (!IGNORE_INSERT_PROPS.Contains(propName))
                    {
                        insFiled.Append($"            \"{c.Name}\",");
                        if (i < table.Columns.Count - 1)
                        {
                            insFiled.AppendLine();
                        }
                    }

                    if (!IGNORE_UPDATE_PROPS.Contains(propName))
                    {
                        if (!(codeParam.IsTenant && "TenantId".Equals(propName)))
                        {
                            updFiled.Append($"            \"{c.Name}\",");
                            if (i < table.Columns.Count - 1)
                            {
                                updFiled.AppendLine();
                            }
                        }
                    }

                    selAllFiled.Append($"            \"{c.Name} {propName}\",");
                    if (i < table.Columns.Count - 1)
                    {
                        selAllFiled.AppendLine();
                    }

                    getValueCase.Append(GetValueCaseTemplate
                        .Replace("|Field|", c.Name)
                        .Replace("|Property|", propName));

                    getValueCase.AppendLine();
                    if (i == table.Columns.Count - 1)
                    {
                        continue;
                    }

                    getValueCase.AppendLine();
                }
            }

            var desc = string.IsNullOrWhiteSpace(table.Description) ? basicName : table.Description;
            return GetClassTemplate(codeParam.Type)
            .Replace("|DbType|", codeParam.Type)
            .Replace("|NamespacePfx|", codeParam.NamespacePfx)
            .Replace("|Description|", desc)
            .Replace("|Name|", name)
            .Replace("|Model|", basicName)
            .Replace("|PkType|", codeParam.PkType.ToCodeString())
            .Replace("|Table|", table.Name)
            .Replace("|InsertFields|", insFiled.ToString())
            .Replace("|UpdateFields|", updFiled.ToString())
            .Replace("|FieldMapProps|", selAllFiled.ToString())
            .Replace("|GetValueCase|", getValueCase.ToString());
        }

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected override string[] SubFolders() => new string[] { "PersistenceInterface", "PersistenceImpl" };

        /// <summary>
        /// 根据类型获取类模板
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类模板</returns>
        private string GetClassTemplate(string type)
        {
            switch (type)
            {
                case "SqlServer":
                    return SqlServerClassTemplate;

                case "MySql":
                    return MySqlClassTemplate;

                default:
                    return null;
            }
        }
    }
}
