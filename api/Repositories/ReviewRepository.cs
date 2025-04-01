using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.DatabaseModels;
namespace WhaleSpottingBackend.Repositories;

public interface IReviewRepository
{

    SightingReview AddSightingReview(SightingReview review);

}

public class ReviewRepository : IReviewRepository
{
    private readonly WhaleSpottingDbContext _context;
    public ReviewRepository(WhaleSpottingDbContext context)
    {
        _context = context;
    }


    public SightingReview AddSightingReview(SightingReview review)
    {
        var insertResult = _context.SightingReview.Add(review);
        _context.SaveChanges();
        return insertResult.Entity;
    }
}
