using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.DatabaseModels;

public class SightingModel
{
    public int Id { get; set; }
    [Column("ImageSource")]
    public string ImageSource { get; set; }
    [Column("Species")]
    public string Species { get; set; }
    [Column("LocationId")]
    public LocationModel LocationId { get; set; }
     [Column("Description")]
    public string Description { get; set; }
    [Column("ReportedBy")]
    public string ReportedBy { get; set; } // user FK
    [Column("StatusId")]
    public int StatusId { get; set; } // status FK
    [Column("SightingDate")]
    public DateTime SightingDate { get; set; }
    [Column("ReportDate")]
    public DateTime ReportDate { get; set; }

    public SightingModel(){}
    public SightingModel(string imageSource, string species, int id, LocationModel locationId, string description, string reportedBy, int statusId, DateTime sightingDate, DateTime reportDate)
    {
        ImageSource = imageSource;
        Species = species;
        Id = id;
        LocationId = locationId;
        Description = description;
        ReportedBy = reportedBy;
        StatusId = statusId;
        SightingDate = sightingDate;
        ReportDate = reportDate;
    }
    
}