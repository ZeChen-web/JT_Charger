using System.Text;
using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Redis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.System;

namespace WebStarter.Controllers.Test;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;


    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("this is a hello world");

        RedisHelper redisHelper = AppInfo.Container.Resolve<RedisHelper>();
        redisHelper.PublishAsync("UploadTelemetryData", JsonConvert.SerializeObject(new { }));

        _logger.LogInformation("this is two hello world");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}