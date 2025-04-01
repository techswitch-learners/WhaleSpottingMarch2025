using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class Location
{
    public int Id { get; set; }
    public Point SpatialCoordinates {get;set;}
   // public Location() { }
    public Location(Point spatialCoordinates)
    {
        SpatialCoordinates = spatialCoordinates;
    }
}