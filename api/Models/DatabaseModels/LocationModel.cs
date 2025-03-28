using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.DatabaseModels;

public class LocationModel
{
    public int Id { get; set; }
    [Column("Latitude")]

    public double Latitude { get; set; }
    [Column("Longitude")]
    public double Longitude { get; set; }

    public LocationModel(){}
    public LocationModel(int id, double latitude, double longitude)
    {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
    }
}