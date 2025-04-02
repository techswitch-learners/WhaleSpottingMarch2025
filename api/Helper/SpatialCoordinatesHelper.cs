using NetTopologySuite.Geometries;
namespace WhaleSpottingBackend.Helper;

public class SpatialCoordinatesHelper
{
    public static Point ConvertLatLonToSpatialCoordinates(double latitude, double longitude)
    {
        return new Point(latitude, longitude) { SRID = 4326 };
    }
}