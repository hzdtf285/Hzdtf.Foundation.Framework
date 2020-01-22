using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 功能持久化
    /// @ 黄振东
    /// </summary>
    public partial class FunctionPersistence
    {
        #region 重写父类的方法

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "menu_function", "function_id" }
            };
        }

        #endregion
    }
}
