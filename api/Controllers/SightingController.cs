using System.Text.Json.Serialization;
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
    private readonly IWebHostEnvironment _webHostEnvironment;
    public SightingController(ISightingRepository sightingRepository, ISpeciesRepository speciesRepository, ILogger<SightingController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _sightingRepository = sightingRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
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
        return sightings.ToList();
    }

    // POST: api/createSighting
    [HttpPost("createSighting")]
    public async Task<IActionResult> CreateSighting([FromForm] CreateSightingRequest sightingRequest, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
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
            ImageSource = imagePath
        };
        var sighting = _sightingRepository.CreateSighting(newSighting);
        var url = Url.Action("GetSightingByID", new { id = sighting.Id });
        var sightingResponse = new SightingResponseModel(sighting);
        return Created(url, sightingResponse);
    }
}
