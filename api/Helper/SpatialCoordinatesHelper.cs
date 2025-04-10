using NetTopologySuite.Geometries;
namespace WhaleSpottingBackend.Helper;

public class SpatialCoordinatesHelper
{
    public static Point ConvertLatLonToSpatialCoordinates(double latitude, double longitude)
    {
        return new Point(latitude, longitude) { SRID = 4326 };
    }

     public static double ConvertRadiusToDegrees(int radius)
    {
        return radius/111;
    }
}
