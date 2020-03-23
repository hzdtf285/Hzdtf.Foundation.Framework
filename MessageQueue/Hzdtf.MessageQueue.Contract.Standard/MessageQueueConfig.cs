using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Factory;
using Hzdtf.Utility.Standard.ProcessCall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard
{
    /// <summary>
    /// 消息队列配置
    /// @ 黄振东
    /// </summary>
    public static class MessageQueueConfig
    {
        /// <summary>
        /// RPC客户端工厂
        /// </summary>
        public static IGeneralFactory<IRpcClient> RpcClientFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 字节数组序列化
        /// </summary>
        public static IBytesSerialization BytesSerialization
        {
            get;
            set;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public static ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;
    }
}
