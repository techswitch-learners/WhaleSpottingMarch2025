using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Sighting {
    public int Id{get;set;}    
    [Column("Species")]
    public Species Species{get;set;}
    [Column("Description")]
    public string Description { get; set; }
    [Column("SightingDate")]
    public DateTime SightingDate { get; set; }
    [Column("ReportDate")]
    public DateTime ReportDate { get; set; }
    [Column("Quantity")]
    public int Quantity { get; set; } 
    [Column("LocationId")]
    public Location Location { get; set; }
    [Column("ImageSource")]
    public string ImageSource { get; set; }
    public Sighting(){}
    public Sighting(int id,Species species, string description, DateTime sightingDate, DateTime reportDate, int quantity, Location location, string imageSource)
    {
        Id = id;
        Species = species;
        Description = description;
        SightingDate = sightingDate;
        ReportDate = reportDate;
        Quantity = quantity;
        Location = location;
        ImageSource = imageSource;
    }
}