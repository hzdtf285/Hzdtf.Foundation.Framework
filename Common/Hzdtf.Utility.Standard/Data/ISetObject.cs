using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 设置对象接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface ISetObject<T>
    {
        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        void Set(T obj);
    }
}
