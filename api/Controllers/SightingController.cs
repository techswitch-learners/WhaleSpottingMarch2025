using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Repositories;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class SightingController : ControllerBase
{
    private readonly ILogger<SightingController> _logger;
    private readonly ISightingRepository _sightingRepository;
    private readonly ISpeciesRepository _speciesRepository;
    public SightingController(ISightingRepository sightingRepository, ISpeciesRepository speciesRepository, ILogger<SightingController> logger)
    {
        _sightingRepository = sightingRepository;
        _speciesRepository = speciesRepository;
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
    public ActionResult<IEnumerable<Sighting>> GetAllSightings()
    {
        var sightings = _sightingRepository.GetAllSightings();
        if (sightings == null)
        {
            return NotFound();
        }
        return sightings.ToList();
    }

    // POST: api/createSighting
    [HttpPost("createSighting")]
    public IActionResult CreateSighting([FromBody] CreateSightingRequest sightingRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Location newLocation = new Location() { Latitude = sightingRequest.Latitude, Longitude = sightingRequest.Longitude };
        Species species = _speciesRepository.GetSpeciesByID(sightingRequest.Species);
        Sighting newSighting = new Sighting
        {
            Species = species,
            Description = sightingRequest.Description,
            SightingDate = sightingRequest.SightingDate,
            ReportDate = sightingRequest.ReportDate,
            Quantity = sightingRequest.Quantity,
            Location = newLocation,
            ImageSource = sightingRequest.ImageSource
        };
        var sighting = _sightingRepository.CreateSighting(newSighting);
        var url = Url.Action("GetSightingByID", new { id = sighting.Id });
        var sightingResponse = new SightingResponseModel(sighting);
        return Created(url, sightingResponse);
    }
}
