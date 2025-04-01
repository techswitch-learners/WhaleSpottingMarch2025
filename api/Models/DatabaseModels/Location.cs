using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Location
{
    public int Id { get; set; }
    [Column("Latitude")]
    public double Latitude { get; set; }
    [Column("Longitude")]
    public double Longitude { get; set; }

    public Point SpatialCoordinates {get;set;}
   // public Location() { }
    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        SpatialCoordinates = new Point(latitude, longitude) { SRID = 4326 };
    }
}