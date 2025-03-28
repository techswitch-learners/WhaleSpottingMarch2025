using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISightingRepository 
{
    Sighting GetSightingByID(int sightingId);
    void PostSighting(CreateSightingRequest sighting);
}