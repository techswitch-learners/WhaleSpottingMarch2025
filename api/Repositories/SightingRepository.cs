using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
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
      
}