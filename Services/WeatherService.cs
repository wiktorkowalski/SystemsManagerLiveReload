using Microsoft.Extensions.Options;

namespace SystemsManagerLiveReload.Services;

public class WeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsSnapshot<WeatherOptions> _optionsSnapshot;

    public WeatherService(IHttpClientFactory httpClientFactory, IOptionsSnapshot<WeatherOptions> optionsSnapshot)
    {
        _httpClientFactory = httpClientFactory;
        _optionsSnapshot = optionsSnapshot;
    }

    public async Task<WeatherDto?> GetWeather()
    {
        if (_optionsSnapshot.Value.ENABLE_API_V3)
        {
            return await GetWeatherV3();
        }

        return await GetWeatherV2();
    }

    private async Task<WeatherDto?> GetWeatherV3()
    {
        var client = _httpClientFactory.CreateClient();
        var response =
            await client.GetAsync(
                $"https://api.openweathermap.org/data/3.0/onecall?lat={33.44}&lon={-94.04}&exclude={"minutely,hourly,daily"}&units=metric&appid={_optionsSnapshot.Value.API_KEY}");

        var weather = await response.Content.ReadFromJsonAsync<WeatherDto>();
        weather!.ApiV3 = true;
        return weather;
    }

    private async Task<WeatherDto?> GetWeatherV2()
    {
        var client = _httpClientFactory.CreateClient();
        var response =
            await client.GetAsync(
                $"https://api.openweathermap.org/data/2.5/onecall?lat={33.44}&lon={-94.04}&exclude={"minutely,hourly,daily"}&units=imperial&appid={_optionsSnapshot.Value.API_KEY}");

        return await response.Content.ReadFromJsonAsync<WeatherDto>();
    }
}

public class WeatherDto
{
    public bool ApiV3 { get; set; } = false;
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    public int TimezoneOffset { get; set; }
    public Current Current { get; set; }    
}


public class Current
{
    public int Dt { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public int Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }
    public Weather[] Weather { get; set; }
}

public class Weather
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

