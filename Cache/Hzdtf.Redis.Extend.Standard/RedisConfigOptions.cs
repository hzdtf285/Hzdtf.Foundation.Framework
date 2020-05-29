using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Safety;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// Redis配置选项
    /// @ 黄振东
    /// </summary>
    public class RedisConfigOptions
    {
        /// <summary>
        /// 连接加密
        /// </summary>
        public bool ConnectionEncrypt
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字典
        /// </summary>
        public RedisConnectionOptions[] Connections { get; set; }

        /// <summary>
        /// 根据键获取连接选项
        /// </summary>
        /// <param name="key">键，如果为空则默认选择第1个</param>
        /// <returns>连接选项</returns>
        public RedisConnectionOptions Get(string key = null)
        {
            if (Connections.IsNullOrLength0())
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                return Connections[0];
            }

            return Connections.Where(p => p.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// 获取明文连接字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>明文连接字符串</returns>
        public string GetPlaintextConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString) || !ConnectionEncrypt)
            {
                return connectionString;
            }

            return DESUtil.Decrypt(connectionString, PlatformTool.AppConfig["DES:Key"], PlatformTool.AppConfig["DES:IV"]);
        }
    }

    /// <summary>
    /// Redis连接选项
    /// @ 黄振东
    /// </summary>
    public class RedisConnectionOptions
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
