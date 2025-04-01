namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingsQueryParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? SpeciesId { get; set; } 
    public bool? HasImage { get; set; }
    public DateTime? SightingStartDate { get; set; }
    public DateTime? SightingEndDate { get; set; } = DateTime.UtcNow;
}
