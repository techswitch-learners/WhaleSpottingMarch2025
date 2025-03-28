using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Models.ApiModels;

public class CreateSightingRequest{
    public string Species { get; set; }
    public string Description { get; set; }
    public DateTime SightingDate { get; set; }
    public DateTime ReportDate;
    public int Quantity { get; set; } 
    public double Latitude{get;set;}
    public double Longitude{get;set;}
    public string ImageSource { get; set; }   
}