using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.RemoteService.Provider
{
    /// <summary>
    /// 服务提供者缓存类型
    /// @ 黄振东
    /// </summary>
    public enum ServiceProviderCacheType : byte
    {
        /// <summary>
        /// 不使用缓存
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 主动刷新
        /// </summary>
        TIMER_REFRESH = 1,

        /// <summary>
        /// 延迟刷新
        /// </summary>
        DALAY_REFRESH = 2
    }
}
