using WhaleSpottingBackend.Models.ApiModels;
namespace WhaleSpottingBackend.Services;

public class WeatherAPI
{
    public readonly HttpClient _httpClient;
    public readonly IConfiguration _config;
    public WeatherAPI(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task<IAsyncEnumerable<DailyWeatherForecastModel>> GetWeatherForecastByLocationAsync (double latitude, double longitude)
    {
        
    }
}
