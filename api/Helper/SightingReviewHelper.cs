using WhaleSpottingBackend.Models.DatabaseModels;

public static class SightingReviewHelper
{
    public static string GetReviewStatus(Sighting sighting)
    {
        return sighting.Reviews?.Any() == false
                ? "Pending"
                : sighting.Reviews?.OrderByDescending(review => review.StatusDate).First().Approved == true ? "Approved" : "Rejected";
    }
}
