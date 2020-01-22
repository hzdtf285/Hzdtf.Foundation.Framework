using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.InterfaceImpl;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.MessageQueue.Contract.Standard.Core
{
    /// <summary>
    /// RPC服务监听
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RpcServerListen : IRpcServerListen
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        /// <summary>
        /// RPC服务
        /// </summary>
        private IRpcServer rpcServer;

        /// <summary>
        /// 是否从单连接里创建
        /// </summary>
        private bool isSingleConnCreated;

        /// <summary>
        /// RPC服务
        /// </summary>
        public IRpcServer RpcServer
        {
            get
            {
                if (rpcServer == null)
                {
                    rpcServer = SingleConnectionTool.Connection.CreateRpcServer(AppConfig["MessageQueue:RpcServer:Queue"]);
                    isSingleConnCreated = true;
                }

                return rpcServer;
            }
            set { this.rpcServer = value; }
        }

        /// <summary>
        /// 字节数组序列化
        /// </summary>
        public IBytesSerialization BytesSerialization
        {
            get;
            set;
        }

        /// <summary>
        /// 接口映射实现
        /// </summary>
        public IInterfaceMapImpl InterfaceMapImpl
        {
            get;
            set;
        }

        /// <summary>
        /// 方法调用
        /// </summary>
        public IMethodCall MethodCall
        {
            get;
            set;
        }

        /// <summary>
        /// 监听
        /// </summary>
        public void Listen()
        {
            var thisFullName = typeof(RpcServerListen).FullName;
            var thisName = typeof(RpcServerListen).Name;

            RpcServer.Receive(inData =>
            {
                object result = null;
                try
                {
                    var rpcDataInfo = BytesSerialization.Deserialize<RpcDataInfo>(inData);
                    if (rpcDataInfo == null)
                    {
                        Log.ErrorAsync("传过来的数据不是RpcDataInfo类型的", null, thisFullName, thisName);
                    }
                    else if (string.IsNullOrWhiteSpace(rpcDataInfo.MethodFullPath))
                    {
                        Log.ErrorAsync("方法全路径不能为空", null, thisFullName, thisName);
                    }
                    else
                    {
                        string classFullName;
                        var methodName = ReflectUtil.GetMethodName(rpcDataInfo.MethodFullPath, out classFullName);

                        var implClassFullName = InterfaceMapImpl.Reader(classFullName);

                        MethodInfo method;
                        var methodReturnValue = MethodCall.Invoke(string.Format("{0}.{1}",implClassFullName, methodName), out method, rpcDataInfo.MethodParams);

                        // 如果方法返回是Void，则直接返回null
                        if (method.IsMethodReturnVoid())
                        {
                            return null;
                        } // 如果方法是异步方法，则转换为Task并等待执行结束后返回Result给客户端
                        else if (method.ReturnType.IsTypeTask())
                        {
                            // 如果带泛型，则返回任务的Result给客户端
                            if (method.ReturnType.IsTypeGenericityTask())
                            {
                                var resultProperty = method.ReturnType.GetProperty("Result");
                                result = resultProperty.GetValue(methodReturnValue);
                            }
                            else
                            {
                                var methodReturnTask = methodReturnValue as Task;
                                methodReturnTask.Wait();
                            }
                        }
                        else
                        {
                            result = methodReturnValue;
                        }
                    }

                    if (result == null)
                    {
                        return null;
                    }

                    return BytesSerialization.Serialize(result);
                }
                catch (Exception ex)
                {
                    try
                    {
                        Log.ErrorAsync(ex.Message, ex, thisFullName, thisName);
                    }
                    catch { }

                    return null;
                }
            });
        }

        /// <summary>
        /// 异步监听
        /// </summary>
        public void ListenAsync()
        {
            Task.Run(() => Listen());
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            RpcServer.Close();

            if (isSingleConnCreated)
            {
                SingleConnectionTool.Connection.Close();
            }

            OnClosed();
        }

        /// <summary>
        /// 执行关闭后事件
        /// </summary>
        protected void OnClosed()
        {
            if (Closed != null)
            {
                Closed(this, new DataEventArgs());
            }
        }
    }
}
