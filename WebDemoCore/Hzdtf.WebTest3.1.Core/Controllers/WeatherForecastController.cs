using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hzdtf.Utility.Standard.RemoteService;
using Hzdtf.Utility.Standard.RemoteService.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hzdtf.WebTest3._1.Core.Controllers
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

        private readonly IUnityServicesBuilder unityServicesBuilder;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnityServicesBuilder unityServicesBuilder)
        {
            _logger = logger;
            this.unityServicesBuilder = unityServicesBuilder;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation(new Exception("异常"), "这是一个测试日剧");

            var url = unityServicesBuilder.BuilderAsync("ServiceExampleA", "/Health", "M1").Result;

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
