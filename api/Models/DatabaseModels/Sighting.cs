using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Sighting
{
    public int Id { get; set; }
    [Column("Species")]
    public Species Species { get; set; }
    public int SpeciesId { get; set; }
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
    public int LocationId { get; set; }
    [Column("ImageSource")]
    public string ImageSource { get; set; }
    public Sighting() { }
    public Sighting(int id, int speciesId, string description, DateTime sightingDate, DateTime reportDate, int quantity, int locationId, string imageSource)
    {
        Id = id;
        SpeciesId = speciesId;
        Description = description;
        SightingDate = sightingDate;
        ReportDate = reportDate;
        Quantity = quantity;
        LocationId = locationId;
        ImageSource = imageSource;
    }
}
