using Hzdtf.CodeGenerator.Contract.Standard.TypeMapper;
using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// 模型生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ModelGeneratorService : FunctionGeneratorBase
    {
        /// <summary>
        /// 类型映射服务工厂
        /// </summary>
        public ISimpleFactory<string, ITypeMapperService> SimpleFactory
        {
            get;
            set;
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
                    classTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Model\\classTemplate.txt".ReaderFileContent();
                }

                return classTemplate;
            }
        }

        /// <summary>
        /// 属性模板
        /// </summary>
        private static string propertyTemplate;

        /// <summary>
        /// 属性模板
        /// </summary>
        private static string PropertyTemplate
        {
            get
            {
                if (propertyTemplate == null)
                {
                    propertyTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Model\\propertyTemplate.txt".ReaderFileContent();
                }

                return propertyTemplate;
            }
        }

        /// <summary>
        /// 忽略的属性名称集合
        /// </summary>
        private static readonly string[] IGNORE_PROP_NAMES = new string[]
        {
            "Id", "TenantId", "CreateTime", "ModifyTime", "CreaterId", "Creater", "ModifierId", "Modifier"
        };

        /// <summary>
        /// 用户忽略的属性名称集合
        /// </summary>
        private static readonly string[] USER_IGNORE_PROP_NAMES = new string[]
        {
            "LoginId", "TenantId", "Password", "Code", "Name", "Enabled", "SystemInlay"
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
            string name = $"{table.Name.FristUpper()}Info";
            string parentClass = null;
            if (codeParam.IsTenant)
            {
                parentClass = "UserInfo".Equals(name) ? $"BasicUserInfo" : $"PersonTimeTenantInfo";
            }
            else
            {
                parentClass = "UserInfo".Equals(name) ? $"BasicUserInfo" : $"PersonTimeInfo";
            }
            EnumGenerator enumGenerator = new EnumGenerator();

            fileNames = new string[] { $"{name}.cs" };
            StringBuilder propCode = new StringBuilder();

            ITypeMapperService typeMapper = SimpleFactory.Create(codeParam.Type);
            if (!table.Columns.IsNullOrCount0())
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    ColumnInfo c = table.Columns[i];
                    string propName = c.Name.FristUpper();
                    if (IGNORE_PROP_NAMES.Contains(propName))
                    {
                        continue;
                    }

                    if ("UserInfo".Equals(name))
                    {
                        if (USER_IGNORE_PROP_NAMES.Contains(propName))
                        {
                            continue;
                        }
                    }

                    string propType = typeMapper.GetPropertyType(c);
                    CommentInfo comment = null;
                    if (!string.IsNullOrWhiteSpace(c.Description))
                    {
                        try
                        {
                            comment = JsonUtil.Deserialize<CommentInfo>(c.Description);
                            if (comment != null && comment.Enum != null)
                            {
                                propType = enumGenerator.BuilderCodeText(comment.Enum, codeParam.NamespacePfx);
                            }
                        }
                        catch { }
                    }

                    string commentDesc = null;
                    StringBuilder attrCode = new StringBuilder();
                    if (!c.IsNull)
                    {
                        attrCode.Append(GetAttrCode("Required"));
                        attrCode.AppendLine();
                    }
                    if (c.Length != null && "string".Equals(propType))
                    {
                        attrCode.Append(GetAttrCode(string.Format("MaxLength({0})", c.Length)));
                        attrCode.AppendLine();
                    }
                    if (comment != null)
                    {
                        if (string.IsNullOrWhiteSpace(comment.Desc))
                        {
                            commentDesc = c.Description;
                        }
                        else
                        {
                            commentDesc = comment.Desc;
                        }

                        if (!string.IsNullOrWhiteSpace(comment.Name))
                        {
                            attrCode.Append(GetAttrCode(string.Format("DisplayName(\"{0}\")", comment.Name)));
                            attrCode.AppendLine();
                        }

                        if (comment.MinLength != null && "string".Equals(propType))
                        {
                            attrCode.Append(GetAttrCode(string.Format("MinLength({0})", comment.MinLength)));
                            attrCode.AppendLine();
                        }

                        if (comment.Range != null && comment.Range.Length == 2)
                        {
                            attrCode.Append(GetAttrCode(string.Format("Range({0}, {1})", comment.Range[0], comment.Range[1])));
                            attrCode.AppendLine();
                        }
                    }
                    else
                    {
                        commentDesc = c.Description;
                    }

                    if (string.IsNullOrWhiteSpace(commentDesc))
                    {
                        commentDesc = propName;
                    }

                    propCode.Append(PropertyTemplate
                        .Replace("|Description|", commentDesc)
                        .Replace("|JsonName|", c.Name.FristLower())
                        .Replace("|Attribute|", attrCode.ToString())
                        .Replace("|Type|", propType)
                        .Replace("|Name|", propName)
                        .Replace("|Order|", (i + 1).ToString()));
                    
                    if (i == table.Columns.Count - 1)
                    {
                        continue;
                    }

                    propCode.AppendLine();
                    propCode.AppendLine();
                }
            }

            var desc = string.IsNullOrWhiteSpace(table.Description) ? name : table.Description;
            return new string[]
            {
                ClassTemplate
                    .Replace("|NamespacePfx|", codeParam.NamespacePfx)
                    .Replace("|Description|", desc)
                    .Replace("|Name|", name)
                    .Replace("|Inherit|", parentClass)
                    .Replace("|PkType|", codeParam.PkType.ToCodeString())
                    .Replace("|Property|", propCode.ToString())
            };
        }

        /// <summary>
        /// 获取特性代码
        /// </summary>
        /// <param name="attrName">特性名称</param>
        /// <returns>特性代码</returns>
        private string GetAttrCode(string attrName) => $"        [{attrName}]";

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected override string[] SubFolders() => new string[] { "Model" };
    }
}
