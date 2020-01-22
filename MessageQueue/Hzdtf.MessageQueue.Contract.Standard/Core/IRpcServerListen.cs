using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Core
{
    /// <summary>
    /// RPC服务监听接口
    /// @ 黄振东
    /// </summary>
    public interface IRpcServerListen : ICloseable
    {
        /// <summary>
        /// 监听
        /// </summary>
        void Listen();

        /// <summary>
        /// 异步监听
        /// </summary>
        void ListenAsync();
    }
}
