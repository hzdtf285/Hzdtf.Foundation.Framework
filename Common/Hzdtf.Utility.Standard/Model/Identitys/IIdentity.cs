using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Identitys
{
    /// <summary>
    /// ID接口
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public interface IIdentity<IdT>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="idStr">ID字符串</param>
        /// <returns>ID值</returns>
        IdT ConvertTo(string idStr);

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns>ID值</returns>
        IdT New();

        /// <summary>
        /// 默认
        /// </summary>
        /// <returns>ID值</returns>
        IdT Default();

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>是否为空</returns>
        bool IsEmpty(IdT id);

        /// <summary>
        /// 获取值SQL
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值SQL</returns>
        string GetValueSql(IdT id);
    }
}
