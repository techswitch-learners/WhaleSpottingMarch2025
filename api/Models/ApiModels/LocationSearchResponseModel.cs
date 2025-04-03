using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class LocationSearchResponseModel
{
<<<<<<< HEAD
    public IEnumerable<TopSpeciesResponseModel> TopSpecies { get; set; }
    public IEnumerable<SightingResponseModel> RecentSightings { get; set; }
=======
    //weather forecast  
    public IEnumerable<TopSpeciesResponseModel> TopSpecies {get;set;}
    public IEnumerable<SightingResponseModel> RecentSightings {get;set;}  
>>>>>>> 97bd8a9 (Updated Location Controller and Sighting Repo)
}
public class TopSpeciesResponseModel
{
    public string Species { get; set; }
    public int NumSightings { get; set; }
    public DateTime LastSeen { get; set; }
}