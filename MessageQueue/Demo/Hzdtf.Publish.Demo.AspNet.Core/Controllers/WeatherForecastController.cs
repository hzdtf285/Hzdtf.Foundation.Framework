using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hzdtf.Rabbit.Impl.Standard;

namespace Hzdtf.Publish.Demo.AspNet.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 默认主的消息队列连接
        /// </summary>
        private IMessageQueueConnection conn;

        /// <summary>
        /// 消息队列连接工厂，可用此工厂创建自定义的消息队列连接
        /// </summary>
        private IMessageQueueConnectionFactory factory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMessageQueueConnection conn, IMessageQueueConnectionFactory factory)
        {
            _logger = logger;
            this.conn = conn;
            this.factory = factory;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var conn2 = factory.CreateConnectionAndOpen();

            var producer = conn.CreateProducer("TestExchange");//此处是交换机名称
            producer.Publish("测试发送消息", "TestKey1");//此处是绑定key

            producer.Close();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
