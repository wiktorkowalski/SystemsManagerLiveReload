using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SystemsManagerLiveReload.Services;

namespace SystemsManagerLiveReload.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : Controller
{
    private readonly IOptionsSnapshot<WeatherOptions> _optionsSnapshot;
    private readonly WeatherService _weatherService;

    public WeatherController(IOptionsSnapshot<WeatherOptions> optionsSnapshot, WeatherService weatherService)
    {
        _optionsSnapshot = optionsSnapshot;
        _weatherService = weatherService;
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetWeather()
    {
        var weather = await _weatherService.GetWeather();
        
        return Ok(weather);
    }
}