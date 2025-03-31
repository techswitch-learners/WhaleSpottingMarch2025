using Microsoft.AspNetCore.Mvc;
namespace WhaleSpottingBackend.Controllers;

using Microsoft.AspNetCore.Authorization;
using WhaleSpottingBackend.Repositories;

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
//Latitude= x = 10.5
//Longitude y = 230.78
//Radius = 10 
//x+10 x-10 y+10 y-10
//
//225.4
    

}