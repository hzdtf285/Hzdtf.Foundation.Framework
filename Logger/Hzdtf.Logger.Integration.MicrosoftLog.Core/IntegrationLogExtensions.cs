using Hzdtf.Logger.Integration.MicrosoftLog.Standard;
using Microsoft.Extensions.Logging;
using System;

namespace Hzdtf.Logger.Integration.MicrosoftLog.Core
{
    /// <summary>
    /// 集成日志扩展类
    /// @ 黄振东
    /// </summary>
    public static class IntegrationLogExtensions
    {
        /// <summary>
        /// 添加Hzdtf
        /// </summary>
        /// <param name="logBuilder">日志生成器</param>
        /// <param name="options">回调选项，如果需要指定原生日志对象，则需要配置；否则使用默认的原生日志</param>
        /// <returns>日志生成器</returns>
        public static ILoggingBuilder AddHzdtf(this ILoggingBuilder logBuilder, Action<IntegrationLogProvider> options = null)
        {
            var provider = new IntegrationLogProvider();
            if (options != null)
            {
                options(provider);
            }

            logBuilder.AddProvider(provider);

            return logBuilder;
        }
    }
}
