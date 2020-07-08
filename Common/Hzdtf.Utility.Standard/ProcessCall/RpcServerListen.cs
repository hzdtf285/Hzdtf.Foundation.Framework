using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.InterfaceImpl;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// Rpc服务监听
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RpcServerListen : IRpcServerListen
    {
        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DataHandler Closed;

        /// <summary>
        /// 字节数组序列化，默认为json序列化
        /// </summary>
        public IBytesSerialization BytesSerialization
        {
            get;
            set;
        } = new JsonBytesSerialization();

        /// <summary>
        /// 接口映射实现，默认为InterfaceMapImplCache
        /// </summary>
        public IInterfaceMapImpl InterfaceMapImpl
        {
            get;
            set;
        } = new InterfaceMapImplCache();

        /// <summary>
        /// 方法调用，默认为MethodCallCache
        /// </summary>
        public IMethodCall MethodCall
        {
            get;
            set;
        } = new MethodCallCache();

        /// <summary>
        /// Rpc服务端
        /// </summary>
        public IRpcServer RpcServer
        {
            get;
            set;
        }

        /// <summary>
        /// 接收中错误事件
        /// </summary>
        public event Action<string, Exception> ReceivingError;

        /// <summary>
        /// 监听
        /// </summary>
        public void Listen()
        {
            RpcServer.Receive(inData =>
            {
                object result = null;
                try
                {
                    var rpcDataInfo = BytesSerialization.Deserialize<RpcDataInfo>(inData);
                    if (rpcDataInfo == null)
                    {
                        OnReceivingError("传过来的数据不是RpcDataInfo类型的");
                    }
                    else if (string.IsNullOrWhiteSpace(rpcDataInfo.MethodFullPath))
                    {
                        OnReceivingError("方法全路径不能为空");
                    }
                    else
                    {
                        string classFullName;
                        var methodName = ReflectUtil.GetMethodName(rpcDataInfo.MethodFullPath, out classFullName);

                        var implClassFullName = InterfaceMapImpl.Reader(classFullName);

                        MethodInfo method;
                        var methodReturnValue = MethodCall.Invoke(string.Format("{0}.{1}", implClassFullName, methodName), out method, rpcDataInfo.MethodParams);

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
                        OnReceivingError(ex.Message, ex);
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
        public virtual void Close()
        {
            RpcServer.Close();

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

        /// <summary>
        /// 执行接收中错误事件
        /// </summary>
        /// <param name="err">错误消息</param>
        /// <param name="ex">异常</param>
        protected void OnReceivingError(string err, Exception ex = null)
        {
            if (ReceivingError != null)
            {
                ReceivingError(err, ex);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }
    }
}
