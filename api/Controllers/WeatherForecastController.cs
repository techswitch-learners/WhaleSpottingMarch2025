using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController:ControllerBase
{
private readonly ILogger<WeatherForecastController> _logger;

public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
}
