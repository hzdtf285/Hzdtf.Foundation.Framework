using Hzdtf.Utility.Standard.Attr;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.SystemV2
{
    /// <summary>
    /// 本地系统时钟
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LocalSystemClock : ISystemClock
    {
        /// <summary>
        /// UTC当前时间
        /// </summary>
        public DateTimeOffset UtcNow => DateTime.Now;
    }
}
