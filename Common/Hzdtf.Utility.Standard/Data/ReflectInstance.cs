using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 反射实例
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ReflectInstance : IInstance
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        public object CreateInstance(string classFullPath)
        {
            return ReflectUtil.CreateInstance(classFullPath);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        public T CreateInstance<T>(string classFullPath)
            where T : class
        {
            return ReflectUtil.CreateInstance<T>(classFullPath);
        }
    }
}
