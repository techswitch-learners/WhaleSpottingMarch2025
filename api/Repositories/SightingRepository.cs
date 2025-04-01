
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository
{
    Sighting GetSightingByID(int sightingId);
    Sighting CreateSighting(Sighting sighting);
    IEnumerable<Sighting> GetSightingsBySearchQuery(SightingsQueryParameters parameters);

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

    public IEnumerable<Sighting> GetSightingsBySearchQuery(SightingsQueryParameters parameters)
    {
        return _context.Sighting
            .Include(sighting => sighting.Location)
            .Include(sighting => sighting.Species)
            .Where(sighting => parameters.SpeciesId == null || sighting.SpeciesId == parameters.SpeciesId)
            .Where(sighting => parameters.HasImage == null ||
                ((bool)parameters.HasImage ? sighting.ImageSource != null : sighting.ImageSource == null))
            .Where(sighting => parameters.SightingStartDate == null ||
                sighting.SightingDate >= parameters.SightingStartDate && sighting.SightingDate <= parameters.SightingEndDate)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    public IEnumerable<Sighting> GetSightingsByLocation(Point geoCoordinate , int radius )
    {
        return _context.Sighting.Include(Models.DatabaseModels.Location.Where(location => location.SpatialCoordinates.IsWithinDistance(geoCoordinate,radius))).ToList();
                               

                               //  var distance = GeoCoordinate.IsWithinDistance(new Point(latitude,longitude), radius);
     // Console.WriteLine(distance);   
        //and sighting.Location.Latitude in (longitude1 and longitude2))
         //   .Include(sighting => sighting.Location)
            //.Include(sighting => sighting.Species)
    }

   /*     public IEnumerable<Location> GetLocation(double latitude1,double latitude2, double longitude1,double longitude2 )
    {
        return _context.Location.Where (location => (location.Latitude > latitude1 and latitude2) )
                                .ToList();
        //and sighting.Location.Latitude in (longitude1 and longitude2))
         //   .Include(sighting => sighting.Location)
            //.Include(sighting => sighting.Species)
    }*/

    public Sighting CreateSighting(Sighting sighting)
    {
        var insertResult = _context.Sighting.Add(sighting);
        _context.SaveChanges();
        return insertResult.Entity;
    }
}
