using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Models.ApiModels;
public class CreateReviewRequest
{
    public required int SightingID { get; set; }
    public required bool Approved { get; set; }
    public required string Comments { get; set; }
    public UpdateSightingRequest? UpdatedSighting { get; set; }
}
