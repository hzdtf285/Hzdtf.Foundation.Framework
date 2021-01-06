using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Identitys
{
    /// <summary>
    /// 字符串ID
    /// </summary>
    [Inject]
    public class StringId : IIdentity<string>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="idStr">ID字符串</param>
        /// <returns>ID值</returns>
        public string ConvertTo(string idStr) => idStr;

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns>ID值</returns>
        public string New() => StringUtil.NewShortGuid();

        /// <summary>
        /// 默认
        /// </summary>
        /// <returns>ID值</returns>
        public string Default() => null;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>是否为空</returns>
        public bool IsEmpty(string id) => string.IsNullOrWhiteSpace(id);

        /// <summary>
        /// 获取值SQL
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值SQL</returns>
        public string GetValueSql(string id) => $"'{id.FillSqlValue()}'";
    }
}
