using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// Redis配置选项解析接口
    /// @ 黄振东
    /// </summary>
    public interface IRedisConfigOptionParse
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>Redis配置选项</returns>
        RedisConfigOptions Parse();
    }
}
