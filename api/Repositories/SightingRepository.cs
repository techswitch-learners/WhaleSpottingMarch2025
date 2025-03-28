using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public class SightingRepository : ISightingRepository
{
        private readonly WhaleSpottingDbContext _context;
        public SightingRepository(WhaleSpottingDbContext context)
        {
            _context = context;
        }
     
        public Sighting GetSightingByID(int id)
        {
           return _context.Sighting.Where(sighting => sighting.Id == id).Include(sighting =>sighting.Location).FirstOrDefault();
        }

        public void PostSighting(CreateSightingRequest sighting)
        {

            var insertLocation = _context.Location.Add(new Location {
                Latitude = sighting.Latitude, 
                Longitude = sighting.Longitude
                });
                var locationId =insertLocation.Entity.Id;
                Console.WriteLine("locationId"+locationId);
 
            var insertSighting = _context.Sighting.Add(new Sighting
            {
                Species = sighting.Species,
                Description = sighting.Description,
                SightingDate = sighting.SightingDate,
                ReportDate = sighting.ReportDate,
                Quantity = sighting.Quantity,
                Location = insertLocation.Entity,
                ImageSource = sighting.ImageSource
            });
              _context.SaveChanges();
        
                Console.WriteLine("insertSighting"+insertSighting.Entity.Id);
        }
}