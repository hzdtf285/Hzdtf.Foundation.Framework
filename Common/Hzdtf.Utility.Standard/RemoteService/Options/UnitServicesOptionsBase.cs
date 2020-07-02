using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace Hzdtf.Utility.Standard.RemoteService.Options
{
    /// <summary>
    /// 统一服务服务配置基类
    /// @ 黄振东
    /// </summary>
    public abstract class UnitServicesOptionsBase : IUnityServicesOptions
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public UnitServicesOptionsBase(string jsonFile = "Config/serviceBuilderConfig.json", bool isExecWrite = true)
        {
            if (isExecWrite)
            {
                InitJsonFile(jsonFile);
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public UnitServicesOptionsBase(UnityServicesOptions options, bool isExecWrite = true)
        {
            if (isExecWrite)
            {
                Write(options);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public abstract UnityServicesOptions Reader();

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="data">数据</param>
        public void Write(UnityServicesOptions data)
        {
            data.Reset();
            WriteToStorage(data);
        }

        /// <summary>
        /// 写入到存储里
        /// </summary>
        /// <param name="data">数据</param>
        protected abstract void WriteToStorage(UnityServicesOptions data);

        /// <summary>
        /// 初始化Json文件
        /// </summary>
        /// <param name="jsonFile">Json文件</param>
        protected void InitJsonFile(string jsonFile)
        {
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                return;
            }

            if (File.Exists(jsonFile))
            {
                var options = JsonUtil.DeserializeFromFile<UnityServicesOptions>(jsonFile);
                Write(options);
            }
        }
    }
}
