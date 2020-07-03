using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hzdtf.MessageQueue.Contract.Standard.Connection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        private IMessageQueueConnection conn;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMessageQueueConnection conn)
        {
            _logger = logger;
            this.conn = conn;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
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
