using System.Security;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        string status = !sighting.Reviews?.Any() == true
                ? "Pending"
                : sighting.Reviews?.OrderByDescending(review => review.StatusDate).First().Approved == true ? "Approved" : "Rejected";

        var response = new SightingResponseModel(sighting, status);
        return response;
    }

    // GET: api/Sightings
    [HttpGet("")]
    public ActionResult<IEnumerable<SightingResponseModel>> GetSightingsBySearchQuery([FromQuery] SightingsQueryParameters parameters)
    {
        var sightings = _sightingRepository.GetSightingsBySearchQuery(parameters);
        if (sightings == null)
        {
            return NotFound();
        }
        List<SightingResponseModel> sightingsList = [];
        foreach (var sighting in sightings)
        {
            string status = !sighting.Reviews?.Any() == true
                ? "Pending"
                : sighting.Reviews?.OrderByDescending(review => review.StatusDate).First().Approved == true ? "Approved" : "Rejected";

            var sightingResponse = new SightingResponseModel(sighting, status);
            sightingsList.Add(sightingResponse);
        }
        return sightingsList.ToList();
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
            ReportDate = DateTime.UtcNow,
            Quantity = sightingRequest.Quantity,
            Location = newLocation,
            ImageSource = sightingRequest.ImageSource,

        };
        var sighting = _sightingRepository.CreateSighting(newSighting);
        var url = Url.Action("GetSightingByID", new { id = sighting.Id });
        var sightingResponse = new SightingResponseModel(sighting);
        return Created(url, sightingResponse);
    }


    //sighting/pending-approval
    [HttpGet("pending-approval")]
    [Authorize(Roles = "Admin")]
    public ActionResult<IEnumerable<Sighting>> GetAllPendingApproval()
    {
        var sightings = _sightingRepository.GetAllPendingApproval();
        if (sightings == null)
        {
            return NotFound();
        }
        return sightings.ToList();
    }
}
