using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// RPC服务端接口
    /// @ 黄振东
    /// </summary>
    public interface IRpcServer : ICloseable, IDisposable
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        void Receive(Func<byte[], byte[]> receiveMessageFun);
    }
}
