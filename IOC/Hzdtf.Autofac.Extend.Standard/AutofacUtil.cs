using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Autofac.Extend.Standard
{
    /// <summary>
    /// Autofac辅助类
    /// @ 黄振东
    /// </summary>
    public static class AutofacUtil
    {
        /// <summary>
        /// 是否能注入
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否能注入</returns>
        public static bool CanInject(Type type)
        {
            if (type.IsClass && !type.IsAbstract)
            {
                return !type.GetCustomAttributes(typeof(InjectAttribute), false).IsNullOrLength0();                
            }

            return false;
        }
    }
}
