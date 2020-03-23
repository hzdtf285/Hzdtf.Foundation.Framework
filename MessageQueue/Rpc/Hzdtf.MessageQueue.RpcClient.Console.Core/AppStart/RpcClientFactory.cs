using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.Utility.Standard.Factory;
using Hzdtf.Utility.Standard.ProcessCall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.RpcClient.Console.Core.AppStart
{
    /// <summary>
    /// Rpc客户端工厂
    /// @ 黄振东
    /// </summary>
    public class RpcClientFactory : IGeneralFactory<IRpcClient>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>产品</returns>
        public IRpcClient Create() => SingleConnectionTool.Connection.CreateRpcClient("RpcServiceQueue");
    }
}
