using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 菜单功能持久化
    /// @ 黄振东
    /// </summary>
    public partial class MenuFunctionPersistence
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
                { "user_menu_function", "menu_function_id" },
                { "role_menu_function", "menu_function_id" }
            };
        }

        #endregion
    }
}
