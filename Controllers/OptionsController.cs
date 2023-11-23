using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SystemsManagerLiveReload.Services;

namespace SystemsManagerLiveReload.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionsController : Controller
{
    private readonly IOptions<WeatherOptions> _options;
    private readonly IOptionsSnapshot<WeatherOptions> _optionsSnapshot;
    private readonly OptionsSingleton _optionsSingleton;

    public OptionsController(
        IOptions<WeatherOptions> options,
        IOptionsSnapshot<WeatherOptions> optionsSnapshot,
        OptionsSingleton optionsSingleton)
    {
        _options = options;
        _optionsSnapshot = optionsSnapshot;
        _optionsSingleton = optionsSingleton;
    }
    
    [HttpGet()]
    public IActionResult GetOptions()
    {
        return Ok(new
        {
            options = _options.Value,
            optionsSnapshot = _optionsSnapshot.Value,
        });
    }
    
    [HttpGet("singleton")]
    public IActionResult GetSingleton()
    {
        return Ok(_optionsSingleton.GetOptionsValues());
    }
}