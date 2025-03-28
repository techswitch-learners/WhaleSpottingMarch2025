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
    public SightingController(ISightingRepository sightingRepository,ILogger<SightingController> logger)
    {
        _sightingRepository = sightingRepository;
        _logger = logger;
    }
    
    // GET: api/Sighting/1
    [HttpGet("{id}")]
    public ActionResult<SightingResponseModel> GetSighting(int id){
        var sighting = _sightingRepository.GetSightingByID(id);
        if (sighting == null){
            return NotFound();
        }
        return new SightingResponseModel(sighting);
    }

    // POST: api/createSighting
    [HttpPost("createSighting")]
    public void CreateSighting(CreateSightingRequest newSighting){
            Location insertLocation = new Location() {Latitude = newSighting.Latitude, Longitude = newSighting.Longitude};
            Species species = _sightingRepository.GetSpeciesByID(newSighting.Species);
           
            Sighting insertsighting = new Sighting
            {
                Species = species,
                Description = newSighting.Description, 
                SightingDate = newSighting.SightingDate,
                ReportDate = newSighting.ReportDate,
                Quantity = newSighting.Quantity,
                Location = insertLocation,
                ImageSource = newSighting.ImageSource
            };

         _sightingRepository.PostSighting(insertsighting); //chnges in postsighting
    }


    /*public Location createNewLocation(CreateSightingRequest newSighting){
        return new Location {
                Latitude =newSighting.Latitude, 
                Longitude =newSighting.Longitude
        };

    }*/
}