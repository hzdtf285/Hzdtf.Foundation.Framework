using Hzdtf.Platform.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard
{
    /// <summary>
    /// 配置辅助类
    /// @ 黄振东
    /// </summary>
    class ConfigUtil
    {
        /// <summary>
        /// 连接加密
        /// </summary>
        public static bool ConnectionEncryption
        {
            get
            {
                return string.IsNullOrWhiteSpace(PlatformTool.AppConfig["MessageQueue:ConnectionEncrypt"]) ? false : Convert.ToBoolean(PlatformTool.AppConfig["MessageQueue:ConnectionEncrypt"]);
            }
        }
    }
}
