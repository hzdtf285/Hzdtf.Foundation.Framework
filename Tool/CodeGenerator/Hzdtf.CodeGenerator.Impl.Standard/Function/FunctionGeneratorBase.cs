using Hzdtf.CodeGenerator.Contract.Standard.Function;
using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.CodeGenerator.Impl.Standard.Function
{
    /// <summary>
    /// 功能生成基类
    /// @ 黄振东
    /// </summary>
    public abstract class FunctionGeneratorBase : IFunctionGeneratorService
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="tables">表信息列表</param>
        /// <param name="functionTypes">功能类型集合</param>
        /// <param name="namespacePfx">命名空间前辍</param>
        /// <param name="type">类型</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Generator(IList<TableInfo> tables, string namespacePfx, string type)
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            string[] fileNames;

            foreach (TableInfo t in tables)
            {
                string[] codeTexts = BuilderCodeTexts(t, namespacePfx, type, out fileNames);

                for (int i = 0; i < codeTexts.Length; i++)
                {
                    string folder = $"{Util.FOLDER_ROOT}{SubFolders()[i]}\\Generators";
                    folder.CreateNotExistsDirectory();

                    $"{folder}\\{fileNames[i]}".WriteFileContent(codeTexts[i]);
                }
            }

            return returnInfo;
        }

        /// <summary>
        /// 生成代码文本集合
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="namespacePfx">命名空间前辍</param>
        /// <param name="type">类型</param>
        /// <param name="fileNames">文件名集合</param>
        /// <returns>代码文本集合</returns>
        protected abstract string[] BuilderCodeTexts(TableInfo table, string namespacePfx, string type, out string[] fileNames);

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected abstract string[] SubFolders();
    }
}
