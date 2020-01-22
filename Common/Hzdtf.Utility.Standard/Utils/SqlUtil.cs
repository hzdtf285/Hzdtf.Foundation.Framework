using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// SQL辅助类
    /// @ 黄振东
    /// </summary>
    public static class SqlUtil
    {
        /// <summary>
        /// 过滤SQL值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>过滤后的SQL值</returns>
        public static string FillSqlValue(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Replace("'", "''");
        }

        /// <summary>
        /// 创建where语句，初始化内容(包含前面空格)：" WHERE 1=1"
        /// </summary>
        /// <returns>where语句</returns>
        public static StringBuilder CreateWhereSql()
        {
            return new StringBuilder(" WHERE 1=1");
        }

        /// <summary>
        /// 连接查询的属性映射字段集合
        /// 带有,号
        /// </summary>
        /// <param name="propFields">属性字段集合，属性与字段用空格分隔</param>
        /// <param name="pfx">前辍</param>
        /// <param name="ignoreId">是否过滤ID</param>
        /// <returns>连接后的查询的属性映射字段集合</returns>
        public static string JoinSelectPropMapFields(this string[] propFields, string pfx = null, bool ignoreId = false)
        {
            if (propFields.IsNullOrLength0())
            {
                return null;
            }

            StringBuilder result = new StringBuilder();
            foreach (string pf in propFields)
            {
                string[] temp = pf.Split(' ');
                if (ignoreId && "Id".Equals(temp[1]))
                {
                    continue;
                }

                result.AppendFormat("{0}{1} {2},", pfx, temp[0], temp[1]);
            }
            if (result.Length > 0)
            {
                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }
    }
}
