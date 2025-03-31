namespace WhaleSpottingBackend.Models.ApiModels;

public class SightingsQueryParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
