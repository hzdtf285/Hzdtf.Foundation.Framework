using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 字节数组序列化接口
    /// @ 黄振东
    /// </summary>
    public interface IBytesSerialization
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的数据</returns>
        byte[] Serialize(object obj);

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>对象</returns>
        T Deserialize<T>(byte[] data);

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        object Deserialize(byte[] data, Type type);
    }
}
