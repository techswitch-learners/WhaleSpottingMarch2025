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

            var date = DateTime.Parse(day.GetProperty("date").GetString()!);
            var description = dailyWeather.GetProperty("condition").GetProperty("text").GetString();
            var minTemperatureInCelcius = dailyWeather.GetProperty("mintemp_c").GetSingle();
            var maxTemperatureInCelcius = dailyWeather.GetProperty("maxtemp_c").GetSingle();
            var visibilityInKm = dailyWeather.GetProperty("avgvis_km").GetSingle();
            var maxWindSpeedInKmPerHour = dailyWeather.GetProperty("maxwind_kph").GetSingle();
            var totalPrecipitationInMilimeters = dailyWeather.GetProperty("totalprecip_mm").GetSingle();

            weatherForecast.Add(new DailyWeatherForecastModel(date, description, minTemperatureInCelcius, maxTemperatureInCelcius, visibilityInKm, maxWindSpeedInKmPerHour, totalPrecipitationInMilimeters));
        }
        return weatherForecast.AsEnumerable();
    }
}
