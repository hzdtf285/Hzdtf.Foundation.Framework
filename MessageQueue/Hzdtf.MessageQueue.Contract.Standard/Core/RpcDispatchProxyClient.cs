using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Proxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.MessageQueue.Contract.Standard.Core
{
    /// <summary>
    /// RPC动态代理客户端
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RpcDispatchProxyClient : BusinessDispatchProxyBase<RpcDispatchProxyClient>
    {
        /// <summary>
        /// 本身全名
        /// </summary>
        private readonly static string thisFullName = typeof(RpcDispatchProxyClient).FullName;

        /// <summary>
        /// 本身名
        /// </summary>
        private readonly static string thisName = typeof(RpcDispatchProxyClient).Name;

        /// <summary>
        /// 执行业务
        /// </summary>
        /// <param name="targetMethod">目标方法</param>
        /// <param name="args">方法参数数组</param>
        /// <returns>业务返回值</returns>
        protected override object InvokeBusiness(MethodInfo targetMethod, object[] args)
        {
            var rpcData = new RpcDataInfo()
            {
                MethodFullPath = string.Format("{0},{1}.{2}", targetMethod.ReflectedType.Assembly.GetName().Name, targetMethod.ReflectedType.FullName, targetMethod.Name),
                MethodParams = args
            };

            var resultBytes = MessageQueueConfig.RpcClient.Call(MessageQueueConfig.BytesSerialization.Serialize(rpcData));

            // 如果返回值是空或者类型是void，则直接返回null
            if (resultBytes.IsNullOrLength0() || targetMethod.ReturnType.IsTypeVoid())
            {
                return null;
            }
            else
            {
                Type targetType = null;

                // 判断返回类型是否Task
                if (targetMethod.ReturnType.IsTypeNotGenericityTask())
                {
                    return null;
                }
                else if (targetMethod.ReturnType.IsTypeGenericityTask())
                {
                    targetType = targetMethod.ReturnType.GetProperty("Result").PropertyType;
                }
                else
                {
                    targetType = targetMethod.ReturnType;
                }

                return MessageQueueConfig.BytesSerialization.Deserialize(resultBytes, targetType);
            }
        }
    }
}
