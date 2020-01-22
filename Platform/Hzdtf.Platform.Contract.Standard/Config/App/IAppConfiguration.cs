using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Config.Contract.Standard.Config.App
{
    /// <summary>
    /// 应用配置接口
    /// @ 黄振东
    /// </summary>
    public interface IAppConfiguration
    {
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        string this[string key] { get; }

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        string DefaultConnectionString { get; }

        /// <summary>
        /// 从库连接字符串
        /// </summary>
        string SlaveConnectionString { get; }

        /// <summary>
        /// 测试默认连接字符串
        /// </summary>
        string TestDefaultConnectionString { get; }

        /// <summary>
        /// 测试从库连接字符串
        /// </summary>
        string TestSlaveConnectionString { get; }

        /// <summary>
        /// 根据名称获取连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        string GetConnectionString(string name);

        /// <summary>
        /// 允许上传的图片扩展名集合
        /// </summary>
        string[] AllowUploadImageExpands { get; }
    }
}
