using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Factory;
using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 默认连接字符串工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DefaultConnectionStringFactory : ISimpleFactory<EnvironmentType, RedisConnectionInfo>
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 连接加密
        /// </summary>
        private bool ConnectionEncryption
        {
            get
            {
                return string.IsNullOrWhiteSpace(AppConfig["Redis:Encrypt"]) ? false : Convert.ToBoolean(AppConfig["Redis:Encrypt"]);
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public RedisConnectionInfo Create(EnvironmentType type)
        {
            RedisConnectionInfo info = new RedisConnectionInfo();
            switch (type)
            {
                case EnvironmentType.PRODUCTION:
                    info.MasterConnectionStrings = GetConnectionStrings("Redis:Production:DefaultConnection");
                    info.SlaveConnectionStrings = GetConnectionStrings("Redis:Production:SlaveConnection");

                    break;

                case EnvironmentType.TEST:
                    info.MasterConnectionStrings = GetConnectionStrings("Redis:Test:DefaultConnection");
                    info.SlaveConnectionStrings = GetConnectionStrings("Redis:Test:SlaveConnection");

                    break;
            }

            return info;
        }

        /// <summary>
        /// 根据键获取连接字符串集合
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>连接字符串集合</returns>
        private string[] GetConnectionStrings(string key)
        {
            string value = AppConfig[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string conns = ConnectionEncryption ? DESUtil.Decrypt(value, PlatformTool.AppConfig["DES:Key"], PlatformTool.AppConfig["DES:IV"]) : value;
            return conns.Split('|');
        }
    }
}
