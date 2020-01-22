using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 读取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IReader<T>
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        T Reader();
    }
}
