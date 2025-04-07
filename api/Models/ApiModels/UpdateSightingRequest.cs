using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Models.ApiModels;
public class UpdateSightingRequest
{
    public Species? Species { get; set; }
    public string? Description { get; set; }
}
