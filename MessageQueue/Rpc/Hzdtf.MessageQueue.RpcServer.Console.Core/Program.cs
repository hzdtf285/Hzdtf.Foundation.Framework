using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.RpcServer.Console.Core.AppStart;
using Hzdtf.Platform.Impl.Core;
using Microsoft.Extensions.Configuration;
using System;

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

            IRpcServerListen listen = AutofacTool.Resolve<IRpcServerListen>();
            listen.ListenAsync();

            System.Console.WriteLine("start MessageQueue RPC Server Service...");
            System.Console.ReadLine();
        }
    }
}
