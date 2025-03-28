using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository 
{
    Sighting GetSightingByID(int sightingId);
    void PostSighting(Sighting sighting);//changes in postsighting
    Species GetSpeciesByID(int speciesId);
}