using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Helper;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
using LocationModel = WhaleSpottingBackend.Models.DatabaseModels.Location;
using WhaleSpottingBackend.Repositories;
using LocationModel = WhaleSpottingBackend.Models.DatabaseModels.Location;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class SightingController : ControllerBase
{
    private readonly ILogger<SightingController> _logger;
    private readonly ISightingRepository _sightingRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILocationRepository _locationRepository;
    public SightingController(ISightingRepository sightingRepository, ISpeciesRepository speciesRepository, ILocationRepository locationRepository, ILogger<SightingController> logger)
    {
        _sightingRepository = sightingRepository;
        _speciesRepository = speciesRepository;
        _locationRepository = locationRepository;
        _logger = logger;
    }

    // GET: api/Sighting/1
    [HttpGet("{id}")]
    public ActionResult<SightingResponseModel> GetSightingByID([FromRoute] int id)
    {
        var sighting = _sightingRepository.GetSightingByID(id);
        if (sighting == null)
        {
            return NotFound();
        }
        return new SightingResponseModel(sighting);
    }

    // GET: api/Sightings
    [HttpGet("")]
    public ActionResult<IEnumerable<Sighting>> GetSightingsBySearchQuery([FromQuery] SightingsQueryParameters parameters)
    {
        var sightings = _sightingRepository.GetSightingsBySearchQuery(parameters);
        if (sightings == null)
        {
            return NotFound();
        }
        return sightings.Select(sighting => new SightingResponseModel(sighting)).ToList();
    }

    // POST: api/createSighting
    [HttpPost("createSighting")]
    public IActionResult CreateSighting([FromBody] CreateSightingRequest sightingRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Point userLocation = SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(sightingRequest.Latitude, sightingRequest.Longitude);
        LocationModel Location = _locationRepository.GetLocationByGeoCoordinates(userLocation);
        if (Location == null)
        {
            Location = new LocationModel(userLocation);
        }
        Species species = _speciesRepository.GetSpeciesByID(sightingRequest.Species);
        Sighting newSighting = new Sighting
        {
            Species = species,
            Description = sightingRequest.Description,
            SightingDate = sightingRequest.SightingDate,
            ReportDate = DateTime.UtcNow,
            Quantity = sightingRequest.Quantity,
            Location = Location,
            ImageSource = sightingRequest.ImageSource
        };
        var sighting = _sightingRepository.CreateSighting(newSighting);
        var url = Url.Action("GetSightingByID", new { id = sighting.Id });
        var sightingResponse = new SightingResponseModel(sighting);
        return Created(url, sightingResponse);
    }
}
