using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.Safety;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.App
{
    /// <summary>
    /// 应用配置基类
    /// @ 黄振东
    /// </summary>
    public abstract class AppConfigurationBase : IAppConfiguration
    {
        #region 属性与字段

        /// <summary>
        /// 连接加密
        /// </summary>
        protected bool ConnectionEncryption
        {
            get
            {
                return string.IsNullOrWhiteSpace(this["ConnectionStrings:Encrypt"]) ? false : Convert.ToBoolean(this["ConnectionStrings:Encrypt"]);
            }
        }

        #endregion

        #region IAppConfiguration 接口

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract string this[string key] { get; }

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public string DefaultConnectionString { get => GetConnectionString("DefaultConnection"); }

        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public string SlaveConnectionString { get => GetConnectionString("SlaveConnection"); }

        /// <summary>
        /// 测试默认连接字符串
        /// </summary>
        public string TestDefaultConnectionString { get => GetConnectionString("TestDefaultConnection"); }

        /// <summary>
        /// 测试从库连接字符串
        /// </summary>
        public string TestSlaveConnectionString { get => GetConnectionString("TestSlaveConnection"); }

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public string GetConnectionString(string name) => FilterConnectionString(GetDirectConnectionString(name));

        /// <summary>
        /// 默认上传图片的扩展名集合
        /// </summary>
        private string[] defaultUploadImageExpands;

        /// <summary>
        /// 默认上传图片的扩展名集合
        /// </summary>
        public string[] AllowUploadImageExpands
        {
            get
            {
                if (defaultUploadImageExpands == null)
                {
                    string str = this["Image:AllowUploadExpands"];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        defaultUploadImageExpands = str.Split(',');
                    }
                }

                return defaultUploadImageExpands;
            }
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 过滤连接字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>过滤后的连接字符串</returns>
        protected string FilterConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }

            return ConnectionEncryption? DESUtil.Decrypt(connectionString) : connectionString;
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public abstract string GetDirectConnectionString(string name);

        #endregion
    }
}
