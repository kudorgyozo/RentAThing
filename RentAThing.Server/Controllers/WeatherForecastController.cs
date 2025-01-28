using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Infrastructure;
using RentAThing.Server.Models;

namespace RentAThing.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> logger;
    private readonly AppDbContext dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;

    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
