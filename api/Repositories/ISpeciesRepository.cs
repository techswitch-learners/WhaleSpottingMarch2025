using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface ISpeciesRepository
{
    Species GetSpeciesByID(int speciesId);
}