using Hzdtf.Platform.Impl.Core;
using Hzdtf.Redis.Extend.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;

namespace Hzdtf.Redis.Extend.Core
{
    /// <summary>
    /// Redis配置选项解析
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RedisConfigOptionParse : IRedisConfigOptionParse
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>Redis配置选项</returns>
        public RedisConfigOptions Parse()
        {
            return PlatformCodeTool.Config.GetSection("Redis").Get<RedisConfigOptions>();
        }
    }
}
