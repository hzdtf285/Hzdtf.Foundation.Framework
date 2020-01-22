using Hzdtf.Configuration.Impl;
using Hzdtf.Configuration.Impl.Config.Core.App;
using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Logger.Text.Impl.Standard;
using Hzdtf.MessageQueue.Contract.Standard;
using Hzdtf.MessageQueue.Contract.Standard.Core;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Impl.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace Hzdtf.Consumer.Demo.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfig();

            Console.WriteLine("Hello World, consumer:");

            ConsumerReceString();
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
        /// 消费者接收字符串
        /// </summary>
        static void ConsumerReceString()
        {
            IConsumer consumer = SingleConnectionTool.Connection.CreateConsumer("TestQueue1");// 此处是队列名
            consumer.Subscribe((string str) =>
            {
                Console.WriteLine("a msg:" + str);
                return true;
            });

            // 退出要把连接都关闭，此处演示为注释
            //SingleConnectionTool.Close();
        }
    }
}
