using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Identitys
{
    /// <summary>
    /// Guid ID
    /// </summary>
    [Inject]
    public class GuidId : IIdentity<Guid>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="idStr">ID字符串</param>
        /// <returns>ID值</returns>
        public Guid ConvertTo(string idStr)
        {
            if (string.IsNullOrWhiteSpace(idStr))
            {
                return Guid.Empty;
            }

            return Guid.Parse(idStr);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns>ID值</returns>
        public Guid New() => Guid.NewGuid();

        /// <summary>
        /// 默认
        /// </summary>
        /// <returns>ID值</returns>
        public Guid Default() => Guid.Empty;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>是否为空</returns>
        public bool IsEmpty(Guid id) => id == Default();

        /// <summary>
        /// 获取值SQL
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值SQL</returns>
        public string GetValueSql(Guid id) => $"'{id.ToString()}'";
    }
}
