using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Models.ApiModels;
public class CreateReviewRequest
{
    public required int ReportID { get; set; }
    public required bool Approved { get; set; }
    public required DateTime StatusDate { get; set; }
    public required string Comments { get; set; }
    public Sighting? UpdatedSighting {get; set;}
    
}
