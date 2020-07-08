using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Hzdtf.MessageQueue.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Impl.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.Utility.Standard.Release;

namespace Hzdtf.Rabbit.Impl.Standard.Connection
{
    /// <summary>
    /// Rabbit RPC 客户端方法
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitRpcClientMethod : IRpcClientMethod, IClose, IDisposable
    {
        /// <summary>
        /// 消息队列连接工厂
        /// </summary>
        private readonly IMessageQueueConnectionFactory connectionFactoy;

        /// <summary>
        /// Rabbit配置读取
        /// </summary>
        private readonly IRabbitConfigReader rabbitConfigReader;

        /// <summary>
        /// JSON字节流序列化
        /// </summary>
        private static readonly IBytesSerialization jsonSerialation = new JsonBytesSerialization();

        /// <summary>
        /// MessagePack字节流序列化
        /// </summary>
        private static readonly IBytesSerialization messagePackSerialztion = new MessagePackBytesSerialization();

        /// <summary>
        /// 虚拟路径映射RPC客户端字典
        /// </summary>
        private readonly static IDictionary<string, IRpcClient> dicVirtualPathMapRpcClient = new Dictionary<string, IRpcClient>();

        /// <summary>
        /// 同步虚拟路径映射RPC客户端字典
        /// </summary>
        private readonly static object syncDicVirtualPathMapRpcClient = new object();

        /// <summary>
        /// 连接列表
        /// </summary>
        private readonly static IList<IMessageQueueConnection> connections = new List<IMessageQueueConnection>();

        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitRpcClientMethod()
            : this (new RabbitConnectionFactory(), new RabbitMessageQueueJson())
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionFactoy">消息队列连接工厂</param>
        /// <param name="rabbitConfigReader">Rabbit配置读取</param>
        public RabbitRpcClientMethod(IMessageQueueConnectionFactory connectionFactoy, IRabbitConfigReader rabbitConfigReader)
        {
            this.connectionFactoy = connectionFactoy;
            this.rabbitConfigReader = rabbitConfigReader;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="message">消息</param>
        /// <returns>返回数据</returns>
        public object Call(MethodInfo method, object message)
        {
            var assemblyName = method.DeclaringType.Assembly.GetName().Name;
            var configInfo = rabbitConfigReader.Reader();
            if (configInfo == null)
            {
                throw new Exception("找不到RabbitConfig配置信息");
            }

            RpcClientAssemblyWrapInfo rpcClientAssemblyWrap;
            var messQueueInfo = GetMessageQueueByAssemblyName(configInfo, assemblyName, out rpcClientAssemblyWrap);
            if (rpcClientAssemblyWrap == null)
            {
                throw new Exception($"找不到程序集[{assemblyName}]的RPC客户端配置信息");
            }

            var byteSeri = GetByteSerialization(rpcClientAssemblyWrap.RpcClientAssembly.DataType);
            var byteData = byteSeri.Serialize(message);

            IRpcClient rpcClient = null;
            if (dicVirtualPathMapRpcClient.ContainsKey(rpcClientAssemblyWrap.RabbitVirtualPath.VirtualPath))
            {
                rpcClient = dicVirtualPathMapRpcClient[rpcClientAssemblyWrap.RabbitVirtualPath.VirtualPath];
            }
            else
            {
                lock (syncDicVirtualPathMapRpcClient)
                {
                    IMessageQueueConnection conn = null;
                    // 如果连接配置信息不全，则找默认的连接对象
                    if (string.IsNullOrWhiteSpace(rpcClientAssemblyWrap.RabbitVirtualPath.ConnectionString) && string.IsNullOrWhiteSpace(rpcClientAssemblyWrap.RabbitVirtualPath.ConnectionStringAppConfigName))
                    {
                        conn = SingleConnectionTool.CreateConnection();
                    }
                    else
                    {
                        conn = connectionFactoy.CreateAndOpen(new ConnectionWrapInfo()
                        {
                            ConnectionString = rpcClientAssemblyWrap.RabbitVirtualPath.ConnectionString,
                            ConnectionStringAppConfigName = rpcClientAssemblyWrap.RabbitVirtualPath.ConnectionStringAppConfigName
                        });
                    }
                    connections.Add(conn);

                    rpcClient = conn.CreateRpcClient(messQueueInfo);
                    try
                    {
                        dicVirtualPathMapRpcClient.Add(rpcClientAssemblyWrap.RabbitVirtualPath.VirtualPath, rpcClient);
                    }
                    catch (ArgumentException) { }
                }
            }
            
            var reData = rpcClient.Call(byteData);

            // 如果返回值是空或者类型是void，则直接返回null
            if (reData.IsNullOrLength0() || method.ReturnType.IsTypeVoid())
            {
                return null;
            }
            else
            {
                Type targetType = null;

                // 判断返回类型是否Task
                if (method.ReturnType.IsTypeNotGenericityTask())
                {
                    return null;
                }
                else if (method.ReturnType.IsTypeGenericityTask())
                {
                    targetType = method.ReturnType.GetProperty("Result").PropertyType;
                }
                else
                {
                    targetType = method.ReturnType;
                }

                return byteSeri.Deserialize(reData, targetType);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            foreach (var item in dicVirtualPathMapRpcClient)
            {
                item.Value.Close();
            }
            foreach (var item in connections)
            {
                item.Close();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ~RabbitRpcClientMethod()
        {
            Close();
        }

        /// <summary>
        /// 根据程序集名获取消息队列信息
        /// </summary>
        /// <param name="rabbitConfig">rabbit配置</param>
        /// <param name="assemblyName">程序集名</param>
        /// <param name="rpcClientAssemblyWrap">rpc客户端程序集包装</param>
        /// <returns>消息队列信息</returns>
        private static MessageQueueInfo GetMessageQueueByAssemblyName(RabbitConfigInfo rabbitConfig, string assemblyName, out RpcClientAssemblyWrapInfo rpcClientAssemblyWrap)
        {
            rpcClientAssemblyWrap = null;
            if (rabbitConfig == null || rabbitConfig.Rabbit.IsNullOrLength0())
            {
                return null;
            }

            foreach (var r in rabbitConfig.Rabbit)
            {
                if (r.Exchanges.IsNullOrCount0())
                {
                    continue;
                }
                
                foreach (var e in r.Exchanges)
                {
                    if (e.Queues.IsNullOrCount0())
                    {
                        continue;
                    }
                    
                    foreach (var q in e.Queues)
                    {
                        if (q.RpcClientAssemblyInfos.IsNullOrLength0())
                        {
                            continue;
                        }

                        var rpcClientAssembly = q.RpcClientAssemblyInfos.Where(p => p.Name == assemblyName).FirstOrDefault();
                        if (rpcClientAssembly == null)
                        {
                            continue;
                        }

                        rpcClientAssemblyWrap = new RpcClientAssemblyWrapInfo()
                        {
                            RpcClientAssembly = rpcClientAssembly,
                            RabbitVirtualPath = r
                        };

                        var msgQueue = e.ToMessageQueue();
                        msgQueue.AutoDelQueue = q.AutoDelQueue;
                        msgQueue.Qos = q.Qos;
                        msgQueue.Queue = q.Name;
                        msgQueue.RoutingKeys = q.RoutingKeys;

                        return msgQueue;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据数据类型获取字节流序列化
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <returns>字节流序列化</returns>
        private IBytesSerialization GetByteSerialization(string dataType)
        {
            if (string.IsNullOrWhiteSpace(dataType))
            {
                return jsonSerialation;
            }

            var type = dataType.ToLower();
            switch (type)
            {
                case "json":

                    return jsonSerialation;

                case "messagepack":

                    return messagePackSerialztion;

                default:

                    throw new NotSupportedException($"不支持数据类型[{dataType}]的字节流序列化");
            }
        }
    }

    /// <summary>
    /// Rpc客户端程序集包装信息
    /// @ 黄振东
    /// </summary>
    class RpcClientAssemblyWrapInfo
    {
        /// <summary>
        /// Rpc客户端程序集对象
        /// </summary>
        public RpcClientAssemblyInfo RpcClientAssembly
        {
            get;
            set;
        }

        /// <summary>
        /// Rabbit虚拟路径对象
        /// </summary>
        public RabbitVirtualPathInfo RabbitVirtualPath
        {
            get;
            set;
        }
    }
}
