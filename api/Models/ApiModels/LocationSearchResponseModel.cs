using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class LocationSearchResponseModel
{
    public IEnumerable<DailyWeatherForecastModel> WeatherForecast { get; set; }
    public IEnumerable<TopSpeciesResponseModel> TopSpecies { get; set; }
    public IEnumerable<SightingResponseModel> RecentSightings { get; set; }

    public LocationSearchResponseModel(IEnumerable<TopSpeciesResponseModel> topSpecies, IEnumerable<SightingResponseModel> recentSightings)
    {
        TopSpecies = topSpecies;
        RecentSightings = recentSightings;
    }
}
public class DailyWeatherForecastModel
{
    public DateTime Date { get; set;}
    public string Description { get; set;}
    public float AverageTemperatureInCelcius { get; set; }
    public float VisibilityInKm { get; set; }
    public float MaxWindSpeedInKmPerHour { get; set; }
    public int ChanceOfRain { get; set; }
    public int ChanceOfSnow { get; set; }
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
