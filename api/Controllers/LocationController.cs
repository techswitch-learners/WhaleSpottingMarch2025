using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Repositories;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Helper;

namespace WhaleSpottingBackend.Controllers;

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

  //GET: api/location?latitude=34.2&longitude=45.6
  [HttpGet("{latitude}/{longitude}")]
  public ActionResult<LocationSearchResponseModel> GetTopSpeciesAndRecentSightingsByLocation(double latitude, double longitude, int radius = 10)
  {
    var userSearchLocation = SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(latitude, longitude);
    var result = _sightingRepository.GetTopSpeciesAndRecentSightingsByLocation(userSearchLocation, radius);
    return Ok(new
    {
      result.TopSpecies,
      result.RecentSightings
    });
  }
}