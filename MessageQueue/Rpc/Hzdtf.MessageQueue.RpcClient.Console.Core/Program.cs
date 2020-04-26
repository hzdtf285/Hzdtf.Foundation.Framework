using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.MessageQueue.Rpc.Business.Contract.Standard;
using Hzdtf.MessageQueue.RpcClient.Console.Core.AppStart;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard.Conversion;
using Hzdtf.Utility.Standard.Proxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

            var proxy = AutofacTool.Resolve<IBusinessDispatchProxy>();
            var conTypeValue = AutofacTool.Resolve<IConvertTypeValue>();
            var stuService = proxy.Create<IStudentService>();

            var a0 = stuService.TestTask5Async();
            a0.Wait();
            var data = conTypeValue.To(a0.Result, typeof(IList<StudentInfo>)) as IList<StudentInfo>;


            var a1 = stuService.TestTask1Async();
            a1.Wait();
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
