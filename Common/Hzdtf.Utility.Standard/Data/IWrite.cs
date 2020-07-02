using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 写入接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IWrite<T>
    {
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="data">数据</param>
        void Write(T data);
    }
}
