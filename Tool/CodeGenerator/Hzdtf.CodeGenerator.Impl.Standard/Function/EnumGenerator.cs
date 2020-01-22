using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// 枚举生成
    /// @ 黄振东
    /// </summary>
    public class EnumGenerator
    {
        /// <summary>
        /// 文件夹根路径
        /// </summary>
        private readonly static string FOLDER_ROOT = $"{Util.FOLDER_ROOT}Model\\Generators\\Enums\\";

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
                    classTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Model\\enumClassTemplate.txt".ReaderFileContent();
                }

                return classTemplate;
            }
        }

        /// <summary>
        /// 子项模板
        /// </summary>
        private static string itemTemplate;

        /// <summary>
        /// 子项模板
        /// </summary>
        private static string ItemTemplate
        {
            get
            {
                if (itemTemplate == null)
                {
                    itemTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Model\\enumItemTemplate.txt".ReaderFileContent();
                }

                return itemTemplate;
            }
        }

        /// <summary>
        /// 已经生成的名称集合
        /// </summary>
        private IList<string> generatorNames = new List<string>();

        /// <summary>
        /// 生成代码文本
        /// </summary>
        /// <param name="enumInfo">枚举信息</param>
        /// <param name="namespacePfx">命名空间前辍</param>
        /// <returns>枚举名</returns>
        public string BuilderCodeText(EnumInfo enumInfo, string namespacePfx)
        {
            string name = $"{enumInfo.Code.FristUpper()}Enum";
            if (generatorNames.Contains(name))
            {
                return name;
            }

            StringBuilder itemCode = new StringBuilder();

            for (int i = 0; i < enumInfo.Items.Length; i++)
            {
                EnumItem enumItem = enumInfo.Items[i];
                itemCode.Append(ItemTemplate
                   .Replace("|Description|", enumItem.Desc)
                   .Replace("|CODE|", enumItem.Code)
                   .Replace("|VALUE|", enumItem.Value.ToString()));

                if (i == enumInfo.Items.Length - 1)
                {
                    continue;
                }

                itemCode.AppendLine();
                itemCode.AppendLine();
            }

            string desc = string.IsNullOrWhiteSpace(enumInfo.Desc) ? name : enumInfo.Desc;
            string codeText = ClassTemplate
                .Replace("|NamespacePfx|", namespacePfx)
                .Replace("|Description|", desc)
                .Replace("|Name|", name)
                .Replace("|Item|", itemCode.ToString());

            FOLDER_ROOT.CreateNotExistsDirectory();

            $"{FOLDER_ROOT}{name}.cs".WriteFileContent(codeText);

            generatorNames.Add(name);

            return name;
        }
    }
}
