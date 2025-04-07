using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using WhaleSpottingBackend.Helper;
namespace WhaleSpottingBackend.Models.DatabaseModels;

public class LocationModel
{
    public int Id { get; set; }
    public Point SpatialCoordinates { get; set; } = SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(0, 0);

    public LocationModel(Point spatialCoordinates)
    {
        SpatialCoordinates = spatialCoordinates;
    }
    public LocationModel(int id, Point spatialCoordinates)
    {
        Id = id;
        SpatialCoordinates = spatialCoordinates;
    }
}