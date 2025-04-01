using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Models.ApiModels;
using WhaleSpottingBackend.Models.DatabaseModels;
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


    // POST: api/update-status
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
            string? adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId is not null)
            {
                SightingReview newReview = new()
                {
                    SightingID = reviewRequest.SightingID,
                    AdminID = adminId,
                    Approved = reviewRequest.Approved,
                    StatusDate = DateTime.UtcNow,
                    Comments = reviewRequest.Comments
                };
                _reviewRepository.AddSightingReview(newReview);

                if (reviewRequest.UpdatedSighting is not null)
                {
                    Sighting sighting = _sightingRepository.GetSightingByID(reviewRequest.SightingID);
                    sighting.Description = reviewRequest.UpdatedSighting.Description is null ?
                        sighting.Description : reviewRequest.UpdatedSighting.Description;
                    sighting.Species = reviewRequest.UpdatedSighting.Species is null ?
                        sighting.Species : reviewRequest.UpdatedSighting.Species;
                    _sightingRepository.UpdateSighting(sighting);
                }

                return Ok("Review completed successfully.");
            }
            else
            {
                return BadRequest("Unauthorized");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to complete review: {ex.Message}");
        }
    }
}
