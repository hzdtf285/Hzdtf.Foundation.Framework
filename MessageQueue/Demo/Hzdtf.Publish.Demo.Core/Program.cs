using Hzdtf.Configuration.Impl.Config.Core.App;
using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Logger.Text.Impl.Standard;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Impl.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace Hzdtf.Publish.Demo.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfig();

            Console.WriteLine("Hello World, publish:");

            PublishString();
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        static void InitConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            PlatformCodeTool.Config = builder.Build();
            PlatformTool.AppConfig = new AppConfiguration();
            LogTool.DefaultLog = new TxtFileLog();
        }

        /// <summary>
        /// 生产者发布字符串
        /// </summary>
        static void PublishString()
        {
            IProducer producer = SingleConnectionTool.Connection.CreateProducer("TestExchange");//此处是交换机名称
            while (true)
            {
                string msg = Console.ReadLine();
                if ("quit".Equals(msg))
                {
                    // 退出要把所有资源都关闭

                    producer.Close();
                    SingleConnectionTool.Close();

                    return;
                }

                producer.Publish(msg, "TestKey1");//此处是绑定key
            }
        }
    }
}
