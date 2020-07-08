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
    public interface IRpcServer : IBytesReceive, ICloseable, IDisposable
    {
    }
}
