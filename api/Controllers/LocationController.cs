using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Repositories;

namespace WhaleSpottingBackend.Controllers;
[ApiController]
[Route("[controller]")]
public class LocationController :ControllerBase
{
    private readonly ILogger<SightingController> _logger;
    private readonly ISightingRepository _sightingRepository;
    private readonly ISpeciesRepository _speciesRepository;

      public LocationController(ISightingRepository sightingRepository, ISpeciesRepository speciesRepository, ILogger<SightingController> logger)
    {
        _sightingRepository = sightingRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

   // GET: api/location?latitude=34.2&longitude=45.6
    [HttpGet("{latitude}/{longitude}")]
    public ActionResult IEnumerable<LocationSearchResponseModel> GetSightingsByLocation([FromQuery] double latitude, double longitude,int radius=10)
    {

      var GeoCoordinate = new Point(latitude, longitude) { SRID = 4326 };
      Console.WriteLine("radius"+radius);  
      var sighting = _sightingRepository.GetSightingsByLocation(GeoCoordinate,radius);
      if (sighting == null)
      {
        return NotFound();
      }
      return IEnumerable<LocationSearchResponseModel>(sighting);
    }

    // GET: api/location?
    [HttpGet("")]
    public bool GetLocation()
    {

      var loc = new Point(51.5539, 0.1447) { SRID = 4326 };
      Console.WriteLine(loc);
      var distance = loc.IsWithinDistance(new Point(56.4907,4.2026), 1);
      Console.WriteLine(distance);
     // loc.IsWithinDistance
      
      /*  int radius = 10;
        var sighting = _sightingRepository.GetSightingsByLocation(latitude,longitude);
        if (sighting == null)
        {
            return NotFound();
        }*/
        return distance;
    }



}