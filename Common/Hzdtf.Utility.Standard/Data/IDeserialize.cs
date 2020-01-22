using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 反序列化接口
    /// @ 黄振东
    /// </summary>
    public interface IDeserialize
    {
        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>对象</returns>
        T Deserialize<T>(string str);
    }
}
