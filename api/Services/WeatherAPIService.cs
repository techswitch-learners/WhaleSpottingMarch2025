using System.Text.Json;
using WhaleSpottingBackend.Models.ApiModels;
namespace WhaleSpottingBackend.Services;

public interface IWeatherService
{
    Task<IEnumerable<DailyWeatherForecastModel>> GetWeatherForecastByLocationAsync(double latitude, double longitude);
}
public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    public WeatherService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task<IEnumerable<DailyWeatherForecastModel>> GetWeatherForecastByLocationAsync(double latitude, double longitude)
    {
        var apiKey = _config["WhaleSpotting:WeatherServiceApiKey"];
        var forecastDays = 7;
        var baseUrl = _config["WeatherServiceAPI:BaseUrl"];
        var weatherApiEndpoint = $"key={apiKey}&q={latitude},{longitude}&days={forecastDays}&tides=no";

        var weatherResponse = await _httpClient.GetAsync(baseUrl + weatherApiEndpoint);
        weatherResponse.EnsureSuccessStatusCode();

        var rawWeatherData = await JsonSerializer.DeserializeAsync<JsonElement>(await weatherResponse.Content.ReadAsStreamAsync());
        var weatherForecast = new List<DailyWeatherForecastModel>();

        foreach (var day in rawWeatherData.GetProperty("forecast").GetProperty("forecastday").EnumerateArray())
        {
            var dailyWeather = day.GetProperty("day");

            weatherForecast.Add(new DailyWeatherForecastModel
            {
                Date = DateTime.Parse(day.GetProperty("date").GetString()!),
                Description = dailyWeather.GetProperty("condition").GetProperty("text").GetString(),
                MinTemperatureInCelcius = dailyWeather.GetProperty("mintemp_c").GetSingle(),
                MaxTemperatureInCelcius = dailyWeather.GetProperty("maxtemp_c").GetSingle(),
                VisibilityInKm = dailyWeather.GetProperty("avgvis_km").GetSingle(),
                MaxWindSpeedInKmPerHour = dailyWeather.GetProperty("maxwind_kph").GetSingle(),
                TotalPrecipitationInMilimeters = dailyWeather.GetProperty("totalprecip_mm").GetSingle(),
            }
            );
        }
        return weatherForecast.AsEnumerable();
    }
}
