using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// Redis连接信息
    /// @ 黄振东
    /// </summary>
    public struct RedisConnectionInfo
    {
        /// <summary>
        /// 主连接字符串集合
        /// </summary>
        public string[] MasterConnectionStrings
        {
            get;
            set;
        }

        /// <summary>
        /// 从连接字符串集合
        /// </summary>
        public string[] SlaveConnectionStrings
        {
            get;
            set;
        }
    }
}
