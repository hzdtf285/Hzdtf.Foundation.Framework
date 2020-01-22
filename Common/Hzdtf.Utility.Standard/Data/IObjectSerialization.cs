using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 对象序列化接口
    /// @ 黄振东
    /// </summary>
    public interface IObjectSerialization
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的数据</returns>
        object SerializeToObject(object obj);

        /// <summary>
        /// 将对象反序列化为对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>对象</returns>
        object DeserializeToObject(object obj);

        /// <summary>
        /// 将对象反序列化为对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        object DeserializeToObject(object obj, Type type);
    }
}
