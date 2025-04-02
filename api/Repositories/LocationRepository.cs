using System.Collections.Generic;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ILocationRepository
{
    LocationModel GetLocationByGeoCoordinates(Point userSightingLocation);
}
public class LocationRepository : ILocationRepository
{
    private readonly WhaleSpottingDbContext _context;
    public LocationRepository(WhaleSpottingDbContext context)
    {
        _context = context;
    }
    public LocationModel GetLocationByGeoCoordinates(Point userSightingLocation)
    {
        return _context.Location.Where(location => location.SpatialCoordinates == userSightingLocation)
                                .FirstOrDefault();
    }
}