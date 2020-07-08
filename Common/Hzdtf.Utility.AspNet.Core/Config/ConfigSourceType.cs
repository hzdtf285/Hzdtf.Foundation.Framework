using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.Config
{
    /// <summary>
    /// 配置来源类型
    /// @ 黄振东
    /// </summary>
    public enum ConfigSourceType : byte
    {
        /// <summary>
        /// JSON文件
        /// </summary>
        JSON_FILE = 0,

        /// <summary>
        /// 微软配置
        /// </summary>
        MICROSOFT_CONFIGURATION = 1
    }
}
