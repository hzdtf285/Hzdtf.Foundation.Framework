using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 证件单元辅助类
    /// @ 黄振东
    /// </summary>
    public static class ClaimUtil
    {
        /// <summary>
        /// 添加证件单元到列表里，忽略空值
        /// </summary>
        /// <param name="claims">证件单元列表</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        public static void Add(this IList<Claim> claims, string type, string value)
        {
            if (claims == null || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            claims.Add(new Claim(type, value));
        }

        /// <summary>
        /// 根据键获取证件单元值，忽略空值
        /// </summary>
        /// <param name="claims">证件单元列表</param>
        /// <param name="type">类型</param>
        /// <returns>证件单元值</returns>
        public static string Get(this IEnumerable<Claim> claims, string type)
        {
            if (claims == null || string.IsNullOrWhiteSpace(type))
            {
                return null;
            }

            var claim = claims.Where(p => p.Type == type).FirstOrDefault();

            return claim != null ? claim.Value : null;
        }
    }
}
