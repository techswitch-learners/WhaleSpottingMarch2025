
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Models.ApiModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository
{
    Sighting GetSightingByID(int sightingId);
    Sighting CreateSighting(Sighting sighting);
    IEnumerable<Sighting> GetSightingsBySearchQuery(SightingsQueryParameters parameters);
    Sighting UpdateSighting(Sighting sighting);
    IEnumerable<Sighting> GetAllPendingApproval();   
    IEnumerable<Sighting> GetSightingsByLocation(Point geoCoordinate , int radius );
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

    public IEnumerable<Sighting> GetSightingsByLocation(Point userSearchLocation, int radius)
    {
       return _context.Sighting.Where(s => s.Location.SpatialCoordinates.IsWithinDistance(userSearchLocation, radius));
    }
    public LocationSearchResponseModel GetTopSpeciesAndRecentSightingsByLocation(Point userSearchLocation, int radius)
    {
        var sightings = GetSightingsByLocation(userSearchLocation, radius);
        var recentSightings = sightings.AsQueryable().Select(sighting => new SightingResponseModel
       {
            Id = sighting.Species.Id,
            SpeciesName = sighting.Species.SpeciesName,
            Description = sighting.Description,
            SightingDate = sighting.SightingDate,
            ReportDate = sighting.ReportDate,
            Quantity = sighting.Quantity,
            Latitude = sighting.Location.SpatialCoordinates.X,
            Longitude = sighting.Location.SpatialCoordinates.Y,
            ImageSource = sighting.ImageSource
        }
        )
        .OrderByDescending(sighting => sighting.SightingDate);

        var topSpecies = sightings.AsQueryable().GroupBy(s => s.Species.SpeciesName)
                                  .Select(group => new TopSpeciesResponseModel
                                        {
                                            Species = group.Key,
                                            NumSightings = group.Count(),
                                            LastSeen = group.Max(sighting => sighting.SightingDate)
                                        })
                                  .OrderByDescending(s => s.NumSightings)
                                  .ThenByDescending(s => s.LastSeen)
                                  .ToList();

        return new LocationSearchResponseModel
        {
            TopSpecies = topSpecies,
            RecentSightings = recentSightings
        };
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
