using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.RpcServer.Console.Core.AppStart;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard.ProcessCall;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using IRpcServerListen = Hzdtf.MessageQueue.Contract.Standard.Core.IRpcServerListen;

namespace Hzdtf.MessageQueue.RpcServer.Console.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json");
            PlatformCodeTool.Config = builder.Build();

            DependencyInjection.RegisterComponents();

            //var server = SingleConnectionTool.Connection.CreateRpcServer("RpcServiceQueue");

            //server.Receive(inData =>
            //{
            //    var msg = Encoding.UTF8.GetString(inData);

            //    var re = Encoding.UTF8.GetBytes("这是一个回复");

            //    return re;
            //});

            IRpcServerListen listen = AutofacTool.Resolve<IRpcServerListen>();
            listen.ListenAsync();

            System.Console.WriteLine("start MessageQueue RPC Server Service...");
            System.Console.ReadLine();
        }
    }
}
