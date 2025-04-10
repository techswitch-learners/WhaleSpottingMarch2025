using WhaleSpottingBackend.Controllers;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingResponseModelWithCount
{
    public int TotalCount { get; set; }
    public IEnumerable<SightingResponseModel> Sightings { get; set; }

    public SightingResponseModelWithCount(int totalCount, IEnumerable<SightingResponseModel> sightings)
    {
        Sightings = sightings;
        TotalCount = totalCount;
    }
    public SightingResponseModelWithCount() { }
}
