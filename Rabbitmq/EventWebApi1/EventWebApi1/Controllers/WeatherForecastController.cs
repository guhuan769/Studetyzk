using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;
using Zack.EventBus;

namespace EventWebApi1.Controllers
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
        private readonly IEventBus eventBus;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IEventBus eventBus)
        {
            _logger = logger;
            this.eventBus = eventBus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //_logger.LogDebug("¿ªÊ¼");
            //_logger.LogError("´íÎó£º1321");
            eventBus.Publish("OrderCreated", new OrderData(888, "heesdfd", DateTime.Now) );
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    public record OrderData(long Id,string Name,DateTime Date);
}