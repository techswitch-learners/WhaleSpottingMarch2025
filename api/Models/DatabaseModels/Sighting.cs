using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Sighting
{
    public int Id { get; set; }
    [Column("Species")]
    public Species? Species { get; set; }
    public int SpeciesId { get; set; }
    [Column("Description")]
    public required string Description { get; set; }
    [Column("SightingDate")]
    public required DateTime SightingDate { get; set; }
    [Column("ReportDate")]
    public required DateTime ReportDate { get; set; }
    [Column("Quantity")]
    public required int Quantity { get; set; }
    [Column("LocationId")]
    public LocationModel? Location { get; set; }
    public int LocationId { get; set; }
    [Column("ImageSource")]
    public string? ImageSource { get; set; }
    public IEnumerable<SightingReview>? Reviews { get; set; }
    [Column("PostedById")]
    public string? PostedById { get; set; } 
    [ForeignKey("PostedById")]
    public User User { get; set; }
    public Sighting() { }
    public Sighting(int id, int speciesId, string description, DateTime sightingDate, DateTime reportDate, int quantity, int locationId, string imageSource, string postedById)
    {        
        Id = id;
        SpeciesId = speciesId;
        Description = description;
        SightingDate = sightingDate;
        ReportDate = reportDate;
        Quantity = quantity;
        LocationId = locationId;
        ImageSource = imageSource;
        PostedById = postedById;
    }
    public string Status()
    {
        return Reviews?.Any() == false
                ? "Pending"
                : Reviews?.OrderByDescending(review => review.StatusDate).First().Approved == true ? "Approved" : "Rejected";
    }
}
