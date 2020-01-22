using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 程序集辅助类
    /// @ 黄振东
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// 装载程序集集合
        /// </summary>
        /// <param name="assemblys">程序集数组</param>
        /// <returns>程序集集合</returns>
        public static Assembly[] Load(params string[] assemblys)
        {
            if (assemblys.IsNullOrLength0())
            {
                return null;
            }

            Assembly[] assemblies = new Assembly[assemblys.Length];
            for (int i = 0; i < assemblies.Length; i++)
            {
                assemblies[i] = Assembly.Load(assemblys[i]);
            }

            return assemblies;
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        /// <param name="typeName">类型名，如果有程序集则用,分隔</param>
        /// <returns>类型</returns>
        public static Type LoadType(string typeName)
        {
            if (typeName.Contains("."))
            {
                string[] temp = typeName.Split(',');
                return Assembly.Load(temp[0]).GetType(temp[1]);
            }
            else
            {
                return Type.GetType(typeName);
            }
        }
    }
}
