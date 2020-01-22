using Hzdtf.Utility.Standard.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 连接转接管理器接口
    /// @ 黄振东
    /// </summary>
    public interface IConnectionMultiplexerManager
    {
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <param name="db">数据库索引</param>
        /// <param name="key">键，如果不为空，则会按取模分区</param>
        /// <returns>数据库</returns>
        IDatabase GetDatabase(AccessMode accessMode = AccessMode.MASTER, int db = -1, string key = null);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        void Close(AccessMode accessMode = AccessMode.MASTER);

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        void CloseAll();
    }
}
