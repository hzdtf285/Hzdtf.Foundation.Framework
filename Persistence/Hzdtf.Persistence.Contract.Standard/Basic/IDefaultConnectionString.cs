using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 默认连接字符串接口
    /// @ 黄振东
    /// </summary>
    public interface IDefaultConnectionString
    {
        /// <summary>
        /// 连接字符串集合
        /// </summary>
        string[] Connections
        {
            get;
        }
    }
}
