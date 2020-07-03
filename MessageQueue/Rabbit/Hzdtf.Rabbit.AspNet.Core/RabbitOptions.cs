using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.AspNet.Core
{
    /// <summary>
    /// Rabbit配置
    /// @ 黄振东
    /// </summary>
    public class RabbitOptions
    {
        /// <summary>
        /// 连接字符串配置名称
        /// </summary>
        public string ConnectionStringConfigName
        {
            get;
            set;
        }

        /// <summary>
        /// 消息队列文件路径
        /// </summary>
        public string MessageQueueFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public DataContentType ContentType
        {
            get;
            set;
        } = DataContentType.JSON;
    }
}
