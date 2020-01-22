using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Language
{
    /// <summary>
    /// 语系库文件夹JSON，默认读取Config/LanguageLibrary中所有.LanguageLibrary.json结尾的文件。也可指定文件夹
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LanguageLibraryFolderJson : IReaderAll<LanguageInfo>
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件夹
        /// </summary>
        public string JsonFolderPath
        {
            get;
            set;
        } = Directory.GetCurrentDirectory() + "/Config/LanguageLibrary";

        #endregion

        #region IReaderAll<LanguageInfo> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public virtual IList<LanguageInfo> ReaderAll()
        {
            // 获取文件夹下所有的JSON文件
            string[] files = Directory.GetFiles(JsonFolderPath, "*.LanguageLibrary.json");
            if (files.IsNullOrLength0())
            {
                return null;
            }

            List<LanguageInfo> result = new List<LanguageInfo>();
            foreach (var f in files)
            {
                var content = File.ReadAllText(f);
                if (string.IsNullOrWhiteSpace(content))
                {
                    continue;
                }

                result.AddRange(JsonUtil.Deserialize<IList<LanguageInfo>>(content));
            }

            return result;
        }

        #endregion
    }
}
