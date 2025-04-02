using WhaleSpottingBackend.Controllers;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingResponseModel
{
    public int Id { get; set; }
    public int SpeciesId { get; set; }
    public string SpeciesName { get; set; }
    public string Description { get; set; }
    public DateTime SightingDate { get; set; }
    public DateTime ReportDate { get; set; }
    public int Quantity { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ImageSource { get; set; }
    public string Status { get; set; }
    public SightingResponseModel(Sighting sighting, string status = "Pending")
    {
        Id = sighting.Id;
        SpeciesId = sighting.Species.Id;
        SpeciesName = sighting.Species.SpeciesName;
        Description = sighting.Description;
        SightingDate = sighting.SightingDate;
        ReportDate = sighting.ReportDate;
        Quantity = sighting.Quantity;
        Latitude = sighting.Location.SpatialCoordinates.X;
        Longitude = sighting.Location.SpatialCoordinates.Y;
        ImageSource = sighting.ImageSource;
        Status = status;
    }

    public SightingResponseModel() { }
}
