using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Helper;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Repositories;

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


    //GET:/Location?latitude=57.47772&longitude=-4.224721&radius=10
    [HttpGet("")]
    public ActionResult<LocationSearchResponseModel> GetTopSpeciesAndRecentSightingsByLocation([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] int radius = 10)
    {
        var userSearchLocation = SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(latitude, longitude);
        var sightings = _sightingRepository.GetSightingsByLocation(userSearchLocation, radius);
        var recentSightings = sightings.Select(sighting => new SightingResponseModel(sighting))
                                                     .ToList()
                                                     .OrderByDescending(sighting => sighting.SightingDate);

        var topSpecies = sightings.GroupBy(s => s.Species.SpeciesName)
                                  .Select(group => new TopSpeciesResponseModel(group.Key.ToString(), (int)group.Count(), (DateTime)group.Max(sighting => sighting.SightingDate)))
                                  .OrderByDescending(s => s.NumSightings)
                                  .ThenByDescending(s => s.LastSeen)
                                  .ToList();

        return Ok(new LocationSearchResponseModel(topSpecies, recentSightings));
    }
}