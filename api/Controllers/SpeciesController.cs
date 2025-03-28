using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Repositories;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class SpeciesController : ControllerBase
{
    private readonly ILogger<SpeciesController> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    public SpeciesController(ISpeciesRepository speciesRepository, ILogger<SpeciesController> logger)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

    // GET: api/Species
    [HttpGet("")]
    public ActionResult<IEnumerable<Species>> GetAllSpecies()
    {
        var species = _speciesRepository.GetAllSpecies();
        if (species == null)
        {
            return NotFound();
        }
        return species.ToList();
    }

    // GET: api/Species/1
    [HttpGet("{id}")]
    public ActionResult<Species> GetSpeciesByID(int id)
    {
        var species = _speciesRepository.GetSpeciesByID(id);
        if (species == null)
        {
            return NotFound();
        }
        return species;
    }
}