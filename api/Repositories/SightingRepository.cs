using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository
{
    Sighting GetSightingByID(int sightingId);
    Sighting CreateSighting(Sighting sighting);
    IEnumerable<Sighting> GetSightingsBySearchQuery(SightingsQueryParameters parameters);
    Sighting UpdateSighting(Sighting sighting);
    IEnumerable<Sighting> GetAllPendingApproval();
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
            .Include(sighting => sighting.Reviews)
            .FirstOrDefault();
    }

    public IEnumerable<Sighting> GetSightingsBySearchQuery(SightingsQueryParameters parameters)
    {
        return _context.Sighting
            .Include(sighting => sighting.Location)
            .Include(sighting => sighting.Species)
            .Include(sighting => sighting.Reviews)
            .Where(sighting => parameters.SpeciesId == null || sighting.SpeciesId == parameters.SpeciesId)
            .Where(sighting => parameters.HasImage == null ||
                ((bool)parameters.HasImage ? sighting.ImageSource != null : sighting.ImageSource == null))
            .Where(sighting => parameters.SightingStartDate == null ||
                sighting.SightingDate >= parameters.SightingStartDate && sighting.SightingDate <= parameters.SightingEndDate)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
            
    }

    public Sighting CreateSighting(Sighting sighting)
    {
        var insertResult = _context.Sighting.Add(sighting);
        _context.SaveChanges();
        return insertResult.Entity;
    }

    public Sighting UpdateSighting(Sighting sighting)
    {
        var updateResult = _context.Sighting.Update(sighting);
        _context.SaveChanges();
        return updateResult.Entity;
    }

    public IEnumerable<Sighting> GetAllPendingApproval()
    {
        return _context.Sighting
            .Include(sighting => sighting.Reviews)
            .Where(sighting => !sighting.Reviews.Any());
    }
}
