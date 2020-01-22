using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// RPC客户端接口
    /// @ 黄振东
    /// </summary>
    public interface IRpcClient : ICloseable
    {
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>返回字节流</returns>
        byte[] Call(byte[] message);
    }
}
