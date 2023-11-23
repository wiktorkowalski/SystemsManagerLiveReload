using Microsoft.Extensions.Options;

namespace SystemsManagerLiveReload.Services;

public class OptionsSingleton
{
    private readonly IOptions<WeatherOptions> _options;
    private readonly IOptionsMonitor<WeatherOptions> _optionsMonitor;

    public OptionsSingleton(
        IOptions<WeatherOptions> options,
        IOptionsMonitor<WeatherOptions> optionsMonitor)
    {
        _options = options;
        _optionsMonitor = optionsMonitor;
    }
    
    public OptionsSingletonValues GetOptionsValues()
    {
        return new OptionsSingletonValues
        {
            IOptions = _options.Value,
            IOptionsMonitor = _optionsMonitor.CurrentValue,
        };
    }
}

public class OptionsSingletonValues
{
    public WeatherOptions IOptions { get; set; }
    public WeatherOptions IOptionsMonitor { get; set; }
}