using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class LocationSearchResponseModel
{
    //weather forecast
    public IEnumerable<TopSpeciesResponseModel> TopSpecies {get;set;}
    public IEnumerable<SightingResponseModel> RecentSightings {get;set;}  
}
public class TopSpeciesResponseModel
{
    public string Species{get;set;}
    public int NumSightings{get;set;}
    public DateTime LastSeen{get;set;}
}