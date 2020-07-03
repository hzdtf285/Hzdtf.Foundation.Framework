using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Consumer.Demo.AspNet.Core
{
    /// <summary>
    /// 消息服务帮助类
    /// @ 黄振东
    /// </summary>
    public static class MessageServiceHelper
    {
        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="app">应用</param>
        public static void StartReceive(IApplicationBuilder app)
        {
            var conn = app.ApplicationServices.GetRequiredService<IMessageQueueConnection>();
            var consumer = conn.CreateConsumer("TestQueue1");
            consumer.Subscribe((string str) =>
            {
                Console.WriteLine("a msg:" + str);
                return true;
            });
        }
    }
}
