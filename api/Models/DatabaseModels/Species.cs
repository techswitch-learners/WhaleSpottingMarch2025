using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Species
{
    public int Id { get; set; }
    [Column("SpeciesName")]
    public string SpeciesName { get; set; }
    public Species() { }
    public Species(int id, string speciesName)
    {
        Id = id;
        SpeciesName = speciesName;
    }
}