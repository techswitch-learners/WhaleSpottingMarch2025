using WhaleSpottingBackend.Models.DatabaseModels;

public class UpdateSightingRequest
{
    public Species? Species { get; set; }
    public string? Description { get; set; }
}
