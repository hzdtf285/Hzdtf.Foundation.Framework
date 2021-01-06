using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Identitys
{
    /// <summary>
    /// 整型ID
    /// </summary>
    [Inject]
    public class IntId : IIdentity<int>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="idStr">ID字符串</param>
        /// <returns>ID值</returns>
        public int ConvertTo(string idStr)
        {
            if (string.IsNullOrWhiteSpace(idStr))
            {
                return 0;
            }

            return Convert.ToInt32(idStr);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns>ID值</returns>
        public int New() => throw new NotImplementedException("未实现该方法");

        /// <summary>
        /// 默认
        /// </summary>
        /// <returns>ID值</returns>
        public int Default() => 0;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>是否为空</returns>
        public bool IsEmpty(int id) => id == Default();

        /// <summary>
        /// 获取值SQL
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值SQL</returns>
        public string GetValueSql(int id) => id.ToString();
    }
}
