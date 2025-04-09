namespace WhaleSpottingBackend.Models.ApiModels;
public class CreateSightingRequest
{
    public required int Species { get; set; }
    public required string Description { get; set; }
    public required DateTime SightingDate { get; set; }
    public required int Quantity { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public string? ImageSource { get; set; }

}