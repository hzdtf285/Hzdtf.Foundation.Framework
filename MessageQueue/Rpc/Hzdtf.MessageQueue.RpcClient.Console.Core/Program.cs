using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.MessageQueue.Rpc.Business.Contract.Standard;
using Hzdtf.MessageQueue.RpcClient.Console.Core.AppStart;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard.Conversion;
using Hzdtf.Utility.Standard.ProcessCall;
using Hzdtf.Utility.Standard.Proxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.MessageQueue.RpcClient.Console.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json");
            PlatformCodeTool.Config = builder.Build();

            DependencyInjection.RegisterComponents();

            //var client = SingleConnectionTool.Connection.CreateRpcClient("RpcServiceQueue");
            //var rep = client.Call(Encoding.UTF8.GetBytes("这是一个测试RPC"));
            //var rep2 = client.Call(null);

            //System.Console.Read();
            var kq = AutofacTool.Resolve<IRpcClientMethod>();
            var proxy = AutofacTool.Resolve<IBusinessDispatchProxy>();
            var conTypeValue = AutofacTool.Resolve<IConvertTypeValue>();
            var stuService = proxy.Create<IStudentService>();

            var k = stuService.Get(1);
            var k2 = stuService.Get(2);


            System.Console.WriteLine("wait...");

            //a0.Wait();
            //var data = conTypeValue.To(a0.Result, typeof(IList<StudentInfo>)) as IList<StudentInfo>;


            //var a1 = stuService.TestTask1Async();
            //a1.Wait();
            //var a3 = stuService.TestTask3Async();
            //a3.Wait();
            //var a2 = stuService.TestTask2Async();
            //var a4 = stuService.TestTask4Async();

            //Task.WhenAll(a1, a2, a3, a4);


            var r1 = stuService.Get(10);
            var r2 = stuService.Query();

            var r3 = stuService.Get2(11);
            var r4 = stuService.Query2();

            var reInfo = stuService.Add(new StudentInfo()
            {
                Id = 1,
                Name = "黄振东"
            });
            var reInfo2 = stuService.Adds(new List<StudentInfo>() {
                new StudentInfo()
                {
                    Id = 1,
                    Name = "黄振东"
                },
                new StudentInfo()
                {
                    Id = 2,
                    Name = "黄振东2"
                }
            });

            System.Console.WriteLine("start MessageQueue RPC Client...");
            System.Console.ReadLine();
        }
    }
}
