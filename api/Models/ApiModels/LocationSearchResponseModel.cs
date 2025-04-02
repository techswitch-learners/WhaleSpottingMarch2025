using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class LocationSearchResponseModel
{
    public IEnumerable<TopSpeciesResponseModel> TopSpecies { get; set; }
    public IEnumerable<SightingResponseModel> RecentSightings { get; set; }

    public LocationSearchResponseModel(IEnumerable<TopSpeciesResponseModel> topSpecies,IEnumerable<SightingResponseModel> recentSightings)
    {
        TopSpecies = topSpecies;
        RecentSightings = recentSightings;
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