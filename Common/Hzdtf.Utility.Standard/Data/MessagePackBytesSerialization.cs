using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 消息包字节数组序列化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class MessagePackBytesSerialization : IBytesSerialization
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的数据</returns>
        public byte[] Serialize(object obj) => obj != null ? MessagePackSerializer.Serialize(obj) : null;

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>对象</returns>
        public T Deserialize<T>(byte[] data) => data.IsNullOrLength0() ? default(T) : MessagePackSerializer.Deserialize<T>(data);

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public object Deserialize(byte[] data, Type type) => data.IsNullOrLength0() ? null : MessagePackSerializer.NonGeneric.Deserialize(type, data);
    }
}
