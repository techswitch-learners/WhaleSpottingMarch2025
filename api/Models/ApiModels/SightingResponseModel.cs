using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingResponseModel{
    public int Id { get;set; }
    public int Species { get;set; }//there as string
    public string Description { get;set; }
    public DateTime SightingDate { get;set; }
    public DateTime ReportDate { get;set; }
    public int Quantity { get;set; }
    public double Latitude { get;set; }
    public double Longitude { get;set; }
    public string ImageSource { get;set; }
    public SightingResponseModel(Sighting sighting) {
       Id = sighting.Id;
       Species = sighting.Species.Id;
       Description = sighting.Description;
       SightingDate = sighting.SightingDate;
       ReportDate = sighting.ReportDate;
       Quantity = sighting.Quantity;
       Latitude = sighting.Location.Latitude;
       Longitude = sighting.Location.Longitude;
       ImageSource = sighting.ImageSource;
    }
    public SightingResponseModel(){}
}