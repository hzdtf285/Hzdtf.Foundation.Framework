using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// Core控制生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class CoreControllerGeneratorService : FunctionGeneratorBase
    { 
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
                    classTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Controller\\Core\\classTemplate.txt".ReaderFileContent();
                }

                return classTemplate;
            }
        }

        /// <summary>
        /// 生成代码文本集合
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="namespacePfx">命名空间前辍</param>
        /// <param name="type">类型</param>
        /// <param name="fileNames">文件名集合</param>
        /// <returns>代码文本集合</returns>
        protected override string[] BuilderCodeTexts(TableInfo table, string namespacePfx, string type, out string[] fileNames)
        {
            string basicName = table.Name.FristUpper();
            string name = $"{basicName}Controller";
            fileNames = new string[] { $"{name}.cs" };

            string desc = string.IsNullOrWhiteSpace(table.Description) ? basicName : table.Description;
            return new string[] 
            {
                ClassTemplate
                .Replace("|NamespacePfx|", namespacePfx)
                .Replace("|Description|", desc)
                .Replace("|Name|", name)
                .Replace("|Model|", basicName)
            };
        }

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected override string[] SubFolders() => new string[] { "CoreController" };
    }
}
