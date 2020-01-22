using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 序列化接口
    /// @ 黄振东
    /// </summary>
    public interface ISerialization
    {
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        string Serialize(object obj);
    }
}
