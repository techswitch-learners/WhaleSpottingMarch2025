using System.Security;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Helper;
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
    private readonly ILocationRepository _locationRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public SightingController(ISightingRepository sightingRepository, ISpeciesRepository speciesRepository, ILocationRepository locationRepository, ILogger<SightingController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _sightingRepository = sightingRepository;
        _speciesRepository = speciesRepository;
        _locationRepository = locationRepository;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: /Sighting/1
    [HttpGet("{id}")]
    public ActionResult<SightingResponseModel> GetSightingByID([FromRoute] int id)
    {
        var sighting = _sightingRepository.GetSightingByID(id);
        if (sighting == null)
        {
            return NotFound();
        }
        var response = new SightingResponseModel(sighting);
        return response;
    }

    // GET: /Sighting
    [HttpGet("")]
    public ActionResult<IEnumerable<SightingResponseModel>> GetSightingsBySearchQuery([FromQuery] SightingsQueryParameters parameters)
    {
        var sightings = _sightingRepository.GetSightingsBySearchQuery(parameters);
        var totalCount = _sightingRepository.GetCountOfSightingsBySearchQuery(parameters);
        if (sightings == null)
        {
            return NotFound();
        }
        var approvedSightings = sightings.Select(sighting => new SightingResponseModel(sighting));
        var response = new SightingResponseModelWithCount(totalCount, approvedSightings);
        return Ok(response);
    }


    // POST: /createSighting
    [HttpPost("createSighting")]
    public async Task<IActionResult> CreateSighting([FromForm] CreateSightingRequest sightingRequest, IFormFile? image)
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

        string? imagePath = null;
        if (image != null)
        {
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            string baseUrl = "http://localhost:5067/";
            string relativePath = Path.Combine("images", fileName);
            imagePath = new Uri(new Uri(baseUrl), relativePath.Replace("\\", "/")).ToString();
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
            ImageSource = imagePath
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
