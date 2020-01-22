using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.Language
{
    /// <summary>
    /// 语系库配置JSON
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LanguageLibraryConfigJson : LanguageLibraryJson
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public override IList<LanguageInfo> ReaderAll()
        {
            if (string.IsNullOrWhiteSpace(JsonFile))
            {
                JsonFile = AppConfig["Language:JsonFile"];
            }

            return base.ReaderAll();
        }
    }
}
