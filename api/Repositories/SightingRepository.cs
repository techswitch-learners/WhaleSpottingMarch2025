using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository
{
    Sighting GetSightingByID(int sightingId);
    Sighting CreateSighting(Sighting sighting);
    IEnumerable<Sighting> GetAllSightings(SightingsQueryParameters parameters);

}

public class SightingRepository : ISightingRepository
{
    private readonly WhaleSpottingDbContext _context;
    public SightingRepository(WhaleSpottingDbContext context)
    {
        _context = context;
    }

    public Sighting GetSightingByID(int id)
    {
        return _context.Sighting.Where(sighting => sighting.Id == id)
            .Include(sighting => sighting.Location)
            .Include(sighting => sighting.Species)
            .FirstOrDefault();
    }

    public IEnumerable<Sighting> GetAllSightings(SightingsQueryParameters parameters)
    {
        return _context.Sighting
            .Include(sighting => sighting.Location)
            .Include(sighting => sighting.Species)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    public Sighting CreateSighting(Sighting sighting)
    {
        var insertResult = _context.Sighting.Add(sighting);
        _context.SaveChanges();
        return insertResult.Entity;
    }
}
