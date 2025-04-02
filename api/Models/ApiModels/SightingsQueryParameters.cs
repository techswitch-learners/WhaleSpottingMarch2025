namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingsQueryParameters
{
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        { return _pageSize; }
        set
        { _pageSize = value < 50 ? _pageSize = value : _pageSize = _maxPageSize; }
    }
    private readonly int _maxPageSize = 50;
    public int? SpeciesId { get; set; }
    public bool? HasImage { get; set; }
    public DateTime? SightingStartDate { get; set; }
    public DateTime? SightingEndDate { get; set; } = DateTime.UtcNow;
}
