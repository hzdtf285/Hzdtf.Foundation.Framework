using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 实例接口
    /// @ 黄振东
    /// </summary>
    public interface IInstance
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        object CreateInstance(string classFullPath);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        T CreateInstance<T>(string classFullPath)
            where T : class;
    }
}
