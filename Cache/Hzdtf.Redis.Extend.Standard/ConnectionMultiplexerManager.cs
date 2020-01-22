using Hzdtf.Utility.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Factory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 连接转接管理器
    /// @ 黄振东
    /// </summary>
    [Inject]
    public sealed partial class ConnectionMultiplexerManager : IConnectionMultiplexerManager, IDisposable
    {
        #region 属性与字段

        /// <summary>
        /// 连接字符串工厂
        /// </summary>
        public ISimpleFactory<EnvironmentType, RedisConnectionInfo> ConnectionStringFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 连接转接器字典（Key：连接字符串，Value：连接转接器）
        /// </summary>
        private static readonly IDictionary<string, IConnectionMultiplexer> dicConnMultis = new Dictionary<string, IConnectionMultiplexer>();

        /// <summary>
        /// 同步连接转接器字典
        /// </summary>
        private static readonly object syncDicConnMultis = new object();

        #endregion

        #region IConnectionMultiplexerManager 接口

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <param name="db">数据库索引</param>
        /// <param name="key">键，如果不为空，则会按取模分区</param>
        /// <returns>数据库</returns>
        public IDatabase GetDatabase(AccessMode accessMode = AccessMode.MASTER, int db = -1, string key = null) => GetConnectionMulti(accessMode, key).GetDatabase(db);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        public void Close(AccessMode accessMode = AccessMode.MASTER)
        {
            string connString = GetConnectionString(accessMode);
            if (string.IsNullOrWhiteSpace(connString))
            {
                return;
            }

            if (dicConnMultis.ContainsKey(connString))
            {
                dicConnMultis[connString].CloseAsync();
            }
        }

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        public void CloseAll()
        {
            if (dicConnMultis.IsNullOrCount0())
            {
                return;
            }

            foreach (KeyValuePair<string, IConnectionMultiplexer> item in dicConnMultis)
            {
                item.Value.CloseAsync();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据访问模式获取连接字符串
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <param name="key">键，如果不为空，则会按取模分区</param>
        /// <returns>连接字符串</returns>
        private string GetConnectionString(AccessMode accessMode = AccessMode.MASTER, string key = null)
        {
            RedisConnectionInfo connInfo = ConnectionStringFactory.Create(UtilTool.CurrEnvironmentType);
            string[] conns = accessMode == AccessMode.MASTER || connInfo.SlaveConnectionStrings.IsNullOrLength0() ? connInfo.MasterConnectionStrings : connInfo.SlaveConnectionStrings;
            if (conns.IsNullOrLength0())
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(key) || conns.Length == 1)
            {
                return conns[0];
            }

            return conns[key.GetHashCode() % conns.Length];
        }

        /// <summary>
        /// 根据访问模式获取连接转接器
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <param name="key">键，如果不为空，则会按取模分区</param>
        /// <returns>连接转接器</returns>
        private IConnectionMultiplexer GetConnectionMulti(AccessMode accessMode = AccessMode.MASTER, string key = null)
        {
            string connString = GetConnectionString(accessMode, key);
            if (dicConnMultis.ContainsKey(connString))
            {
                return dicConnMultis[connString];
            }

            lock (syncDicConnMultis)
            {
                IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(connString);
                try
                {
                    dicConnMultis.Add(connString, connectionMultiplexer);

                    return connectionMultiplexer;
                }
                catch (ArgumentException)
                {
                    connectionMultiplexer.Close();
                    return dicConnMultis[connString];
                }
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            CloseAll();
        }

        #endregion

        /// <summary>
        /// 析构方法
        /// </summary>
        ~ConnectionMultiplexerManager() => Dispose();
    }
}
