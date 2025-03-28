using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
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
        Console.WriteLine("Location");
        Console.WriteLine(sighting.Location.Latitude);
        // Console.WriteLine("LAtitude",+sighting.Location);
        if (sighting == null){
            return NotFound();
        }
        return new SightingResponseModel(sighting);
    }

    // POST: api/Sighting
  /*  [HttpPost("postsighting")]
    public IActionResult PostSighting(SightingRequestModel newSighting){
        var sighting = _sightingRepository.CreateSighting(newSighting);
        var responseViewModel = new UserResponse(user);
        return Created()
    }*/


     

}
