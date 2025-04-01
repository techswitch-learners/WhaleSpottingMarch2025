using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Repositories;


namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ILogger<ReviewController> _logger;
    private readonly ISightingRepository _sightingRepository;
    private readonly IReviewRepository _reviewRepository;
    public ReviewController(IReviewRepository reviewRepository, ISightingRepository sightingRepository, ILogger<ReviewController> logger)
    {
        _reviewRepository = reviewRepository;
        _sightingRepository = sightingRepository;
        _logger = logger;
    }


    // POST: api/updatereviewstatus
    [HttpPost("update-status")]
    [Authorize(Roles = "Admin")]

    public IActionResult UpdateStatus([FromBody] CreateReviewRequest reviewRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

       try
        {   
            // var user = await _userManager.GetUserAsync(HttpContext.User); 
            var adminId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            SightingReview newReview = new SightingReview
            {
                ReportID = reviewRequest.ReportID,
                AdminID = int.Parse(adminId),
                Approved = reviewRequest.Approved,
                StatusDate = reviewRequest.StatusDate,
                Comments = reviewRequest.Comments
            };

                _reviewRepository.AddSightingReview(newReview);

        // if (reviewRequest.UpdatedSighting != null)
        // {
        //     _sightingRepository.UpdateSighting(reviewRequest.UpdatedSighting);
        // }

            return Ok("Review completed successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception
            return BadRequest($"Failed to complete review: {ex.Message}");
        }
    }
}
