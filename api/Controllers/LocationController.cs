using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Helper;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Repositories;
using WhaleSpottingBackend.Helper;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<SightingController> _logger;
    private readonly ISightingRepository _sightingRepository;

    public LocationController(ISightingRepository sightingRepository, ILogger<SightingController> logger)
    {
        _sightingRepository = sightingRepository;
        _logger = logger;
    }

    //GET: api/location?latitude=34.2&longitude=45.6&radius=5
    [HttpGet("{latitude}/{longitude}")]
    public ActionResult<LocationSearchResponseModel> GetTopSpeciesAndRecentSightingsByLocation(double latitude, double longitude, int radius = 10)
    {
        var userSearchLocation = SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(latitude, longitude);
        var sightings = _sightingRepository.GetSightingsByLocation(userSearchLocation, radius);
        var recentSightings = sightings.Select(sighting => new SightingResponseModel(sighting))
                                                     .ToList()
                                                     .OrderByDescending(sighting => sighting.SightingDate);

        var topSpecies = sightings.GroupBy(s => s.Species.SpeciesName)
                                  .Select(group => new TopSpeciesResponseModel
                                  {
                                      Species = group.Key,
                                      NumSightings = group.Count(),
                                      LastSeen = group.Max(sighting => sighting.SightingDate)
                                  })
                                   .OrderByDescending(s => s.NumSightings)
                                   .ThenByDescending(s => s.LastSeen)
                                   .ToList();

        var result = new LocationSearchResponseModel
        {
            TopSpecies = topSpecies,
            RecentSightings = recentSightings
        };

        return Ok(result);
    }
}