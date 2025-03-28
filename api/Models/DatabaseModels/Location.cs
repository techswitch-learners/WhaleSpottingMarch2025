using System.ComponentModel.DataAnnotations.Schema;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Location {
    public int Id { get; set; }
    [Column("Latitude")]
    public double Latitude { get; set; }
    [Column("Longitude")]
    public double Longitude { get; set; }
    public Location(){}
    public Location(int id, double latitude, double longitude)
    {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;  
    }
}