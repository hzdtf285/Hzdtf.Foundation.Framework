using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 获取对象接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IGetObject<T>
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>
        T Get();
    }
}
