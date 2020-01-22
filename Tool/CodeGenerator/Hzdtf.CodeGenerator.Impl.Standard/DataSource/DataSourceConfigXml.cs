using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data.Dic;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hzdtf.CodeGenerator.Impl.Standard.DataSource
{
    /// <summary>
    /// 数据源配置XML
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DataSourceConfigXml : DictionaryXml
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DataSourceConfigXml()
            : base($"{AppContext.BaseDirectory}Config\\dataSourceConfig.xml")
        {
        }
    }
}
