using Dapper;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 数据字典持久化
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryPersistence
    {
        #region 重写父类的方法

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected override string[] GetPageKeywordFields() => new string[]
        {
            GetFieldByProp("Code"),
            GetFieldByProp("Name")
        };

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "data_dictionary_item", "data_dictionary_id" }
            };
        }

        #endregion
    }
}
