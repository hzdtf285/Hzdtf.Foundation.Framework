using Hzdtf.Utility.Standard.Attr;
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
        /// Redis配置选项解析
        /// </summary>
        public IRedisConfigOptionParse RedisConfigOptionParse
        {
            get;
            set;
        }

        /// <summary>
        /// 连接转接器字典（Key：连接键，Value：连接转接器）
        /// </summary>
        private static readonly IDictionary<string, IConnectionMultiplexer> dicConnMultis = new Dictionary<string, IConnectionMultiplexer>();

        /// <summary>
        /// 同步连接转接器字典
        /// </summary>
        private static readonly object syncDicConnMultis = new object();

        /// <summary>
        /// Redis配置选项
        /// </summary>
        private RedisConfigOptions _RedisConfigOptions;

        /// <summary>
        /// Redis配置选项
        /// </summary>
        private RedisConfigOptions RedisConfigOptions
        {
            get
            {
                if (_RedisConfigOptions == null)
                {
                    _RedisConfigOptions = RedisConfigOptionParse.Parse();
                }

                return _RedisConfigOptions;
            }
        }

        #endregion

        #region IConnectionMultiplexerManager 接口

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="connKey">连接键</param>
        /// <param name="db">数据库索引</param>
        /// <returns>数据库</returns>
        public IDatabase GetDatabase(string connKey = null, int db = -1) => GetConnectionMultiplexer(connKey).GetDatabase(db);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="connKey">连接键</param>
        public void Close(string connKey = null)
        {
            var options = RedisConfigOptions.Get(connKey);
            if (options == null)
            {
                return;
            }
            
            if (dicConnMultis.ContainsKey(options.Key))
            {
                dicConnMultis[connKey].CloseAsync();
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
        /// 根据连接键获取连接转接器
        /// </summary>
        /// <param name="connKey">连接键</param>
        /// <returns>连接转接器</returns>
        public IConnectionMultiplexer GetConnectionMultiplexer(string connKey = null)
        {
            var options = RedisConfigOptions.Get(connKey);
            if (options == null)
            {
                throw new NotImplementedException($"未找到[{connKey}]的连接键的配置");
            }

            if (dicConnMultis.ContainsKey(options.Key))
            {
                return dicConnMultis[options.Key];
            }

            var connString = RedisConfigOptions.GetPlaintextConnectionString(options.ConnectionString);
            lock (syncDicConnMultis)
            {
                IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(connString);
                try
                {
                    dicConnMultis.Add(options.Key, connectionMultiplexer);

                    return connectionMultiplexer;
                }
                catch (ArgumentException)
                {
                    connectionMultiplexer.Close();
                    return dicConnMultis[options.Key];
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
