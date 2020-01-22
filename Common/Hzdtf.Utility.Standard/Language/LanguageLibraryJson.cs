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
    /// 语系库JSON，需要指定JSON文件
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LanguageLibraryJson : IReaderAll<LanguageInfo>
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件
        /// </summary>
        public string JsonFile
        {
            get;
            set;
        }

        #endregion

        #region IReaderAll<LanguageInfo> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public virtual IList<LanguageInfo> ReaderAll()
        {
            if (string.IsNullOrWhiteSpace(JsonFile))
            {
                return null;
            }

            var content = File.ReadAllText(JsonFile);
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            // 如果最后一个字符是;或,，则去掉
            if (';'.Equals(content[content.Length - 1]) || ','.Equals(content[content.Length - 1]))
            {
                content = content.Remove(content.Length - 1, 1);
            }

            return JsonUtil.Deserialize<IList<LanguageInfo>>(content);
        }

        #endregion
    }
}
