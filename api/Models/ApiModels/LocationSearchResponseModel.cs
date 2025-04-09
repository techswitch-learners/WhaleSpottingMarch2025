using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class LocationSearchResponseModel
{
    public IEnumerable<DailyWeatherForecastModel> WeatherForecast { get; set; }
    public IEnumerable<TopSpeciesResponseModel> TopSpecies { get; set; }
    public IEnumerable<SightingResponseModel> RecentSightings { get; set; }

    public LocationSearchResponseModel(IEnumerable<DailyWeatherForecastModel> weatherForecast, IEnumerable<TopSpeciesResponseModel> topSpecies, IEnumerable<SightingResponseModel> recentSightings)
    {
        WeatherForecast = weatherForecast;
        TopSpecies = topSpecies;
        RecentSightings = recentSightings;
    }
}
public class DailyWeatherForecastModel
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public float MinTemperatureInCelcius { get; set; }
    public float MaxTemperatureInCelcius { get; set; }
    public float VisibilityInKm { get; set; }
    public float MaxWindSpeedInKmPerHour { get; set; }
    public float TotalPrecipitationInMilimeters { get; set; }

    public DailyWeatherForecastModel(DateTime date, string description, float minTemperatureInCelcius, float maxTemperatureInCelcius, float visibilityInKm, float maxWindSpeedInKmPerHour, float totalPrecipitationInMilimeters)
    {
        Date = date;
        Description = description;
        MinTemperatureInCelcius = minTemperatureInCelcius;
        MaxTemperatureInCelcius = maxTemperatureInCelcius;
        VisibilityInKm = visibilityInKm;
        MaxWindSpeedInKmPerHour = maxWindSpeedInKmPerHour;
        TotalPrecipitationInMilimeters = totalPrecipitationInMilimeters;
    }
}
public class TopSpeciesResponseModel
{
    public string Species { get; set; }
    public int NumSightings { get; set; }
    public DateTime LastSeen { get; set; }

    public TopSpeciesResponseModel(string species, int numSightings, DateTime lastSeen)
    {
        Species = species;
        NumSightings = numSightings;
        LastSeen = lastSeen;
    }
}
