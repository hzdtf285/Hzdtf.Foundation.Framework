using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard
{
    /// <summary>
    /// 消息队列辅助类
    /// @ 黄振东
    /// </summary>
    public static class MessageQueueUtil
    {
        /// <summary>
        /// 连接是否加密
        /// </summary>
        /// <param name="appConfig">应用配置</param>
        /// <returns>连接是否加密</returns>
        public static bool ConnectionEncrypt(IAppConfiguration appConfig = null)
        {
            if (appConfig == null)
            {
                appConfig = PlatformTool.AppConfig;
            }

            return string.IsNullOrWhiteSpace(appConfig["MessageQueue:ConnectionEncrypt"]) ? false : Convert.ToBoolean(appConfig["MessageQueue:ConnectionEncrypt"]);
        }
    }
}
