using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// 服务生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ServiceGeneratorService : FunctionGeneratorBase
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
                    interfaceTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Service\\interfaceTemplate.txt".ReaderFileContent();
                }

                return interfaceTemplate;
            }
        }

        /// <summary>
        /// 类模板
        /// </summary>
        private static string classTemplate;

        /// <summary>
        /// 类模板
        /// </summary>
        private static string ClassTemplate
        {
            get
            {
                if (classTemplate == null)
                {
                    classTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Service\\classTemplate.txt".ReaderFileContent();
                }

                return classTemplate;
            }
        }

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
            string name = $"I{basicName}Service";
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
            string name = $"{basicName}Service";
            fileName = $"{name}.cs";

            var desc = string.IsNullOrWhiteSpace(table.Description) ? basicName : table.Description;
            return ClassTemplate
                .Replace("|NamespacePfx|", codeParam.NamespacePfx)
                .Replace("|Description|", table.Description)
                .Replace("|Name|", name)
                .Replace("|Model|", basicName)
                .Replace("|PkType|", codeParam.PkType.ToCodeString());
        }

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected override string[] SubFolders() => new string[] { "ServiceInterface", "ServiceImpl" };
    }
}
