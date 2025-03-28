using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WhaleSpottingDbContext _context;
    public SpeciesRepository(WhaleSpottingDbContext context)
    {
        _context = context;
    }

    public Species GetSpeciesByID(int id)
    {
        return _context.Species.Where(species => species.Id == id).FirstOrDefault();
    }
}