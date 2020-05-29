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
        /// <param name="connKey">连接键</param>
        /// <param name="db">数据库索引</param>
        /// <returns>数据库</returns>
        IDatabase GetDatabase(string connKey = null, int db = -1);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="connKey">连接键</param>
        void Close(string connKey = null);

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        void CloseAll();

        /// <summary>
        /// 根据连接键获取连接转接器
        /// </summary>
        /// <param name="connKey">连接键</param>
        /// <returns>连接转接器</returns>
        IConnectionMultiplexer GetConnectionMultiplexer(string connKey = null);
    }
}
